using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomsController : MonoBehaviour
{
	// Player's FPS object 
	public GameObject m_Player;

	// Root object
	GameObject m_RoomsRoot;

	class RoomsData
	{
		public Room roomTemplate;
		public List<Room> instantiatedRooms = new List<Room>();
	}
	Dictionary<string, RoomsData> m_Rooms = new Dictionary<string, RoomsData>();


	static string[] s_RoomNames = new string[]{"Room1", "Room2", "Room3"};
	const int kLevelLoadUpdateCount = 5;
	int m_RoomsLoaded;

	Room m_CurrentRoom;

	static private RoomsController s_Instance = null;

	public static RoomsController instance
	{
		get { return s_Instance; }
	}

	void LoadRoom (string roomName)
	{
	}

	void LoadRooms()
	{
		foreach(var roomName in s_RoomNames)
		{
			Application.LoadLevelAdditiveAsync (roomName);
		}
	}

	Room GetRoomInstance(string roomName)
	{
		RoomsData rd = m_Rooms[roomName];

		// TODO: reuse rooms here
		GameObject room = (GameObject)GameObject.Instantiate(rd.roomTemplate.gameObject);
		rd.instantiatedRooms.Add(room.GetComponent<Room>());

		Transform roomTransform = room.transform;
		roomTransform.SetParent(m_RoomsRoot.transform);

		return room.GetComponent<Room>();
	}

	void LinkRooms(Room room1, Connector connector1, Room room2, Connector connector2)
	{
		Vector3 p1 = connector1.transform.position;
		Vector3 p2 = connector2.transform.position;

		Vector3 d2 = room2.transform.position - p2;
		room2.transform.position = p1 + d2;

		connector1.m_Room = room2;
		connector2.m_Room = room1;
	}

	void BuildRoomsChain(string prevRoomName, Room room)
	{
		string roomName = room.gameObject.name;
		room.gameObject.SetActive(true);
		foreach(var connector in room.m_Connectors)
		{
			if (prevRoomName != null && prevRoomName.StartsWith(connector.m_RoomName))
				continue;

			Room r = GetRoomInstance(connector.m_RoomName);
			Connector c = r.GetConnector(connector.m_ConnectorIndex);
			LinkRooms(room, connector, r, c);

			if (roomName.StartsWith(connector.m_RoomName))
			{
				// Looping
				r.gameObject.SetActive(true);
			}
			else
			{
				BuildRoomsChain(roomName, r);
			}
		}
	}

	void SetupRooms()
	{
		// Attach rooms to the root
		foreach(var roomName in s_RoomNames)
		{
			var roomGO = GameObject.Find(roomName);
			if (roomGO == null)
				continue;
			Room room = roomGO.GetComponent<Room>();
			if (room == null)
				continue;

			room.gameObject.SetActive(false);
			RoomsData rd = new RoomsData();
			rd.roomTemplate = room;
			m_Rooms.Add(roomName, rd);

			Transform roomTransform = room.transform;
			roomTransform.SetParent(m_RoomsRoot.transform);
		}

		// Build level
		m_CurrentRoom = GetRoomInstance("Room1");
		BuildRoomsChain(null, m_CurrentRoom);

		// Move player to the first room's anchor
		var firstRoomTransform = m_CurrentRoom.transform;
		var playerAnchorTransform = firstRoomTransform.Find("Anchors/Player");
		m_Player.transform.position = playerAnchorTransform.position;
		// Activate player
		m_Player.SetActive(true);
	}

	// Use this for initialization
	void Start()
	{
		if (s_Instance == null)
			s_Instance = this;
		m_RoomsRoot = gameObject;
		m_RoomsLoaded = 0;

		// TODO: Move to loading screen
		// Load initial level layout
		LoadRooms();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (m_RoomsLoaded <= kLevelLoadUpdateCount)
		{
			if (m_RoomsLoaded == kLevelLoadUpdateCount)
				SetupRooms();

			m_RoomsLoaded++;
			return;
		}
	}

	public void OnRoomExit(Room room, Connector connector)
	{
		if (m_CurrentRoom == connector.m_Room)
		{
			// Looping
			Connector connectorTo = connector.m_Room.GetConnector(connector.m_ConnectorIndex);
			Vector3 d = m_Player.transform.position - connector.transform.position;
			m_Player.transform.position = connectorTo.transform.position + d;
		}

		m_CurrentRoom = room;
	}
}
