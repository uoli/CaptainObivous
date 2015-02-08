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


	static string[] s_RoomNames = new string[]{"Denial", "Room2", "Room3"};
	const int kLevelLoadUpdateCount = 5;
	int m_RoomsLoaded;

	Room m_CurrentRoom;

	static private RoomsController s_Instance = null;

	public static RoomsController GetInstance()
	{
		return s_Instance;
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
		RoomsData rd;
		if (!m_Rooms.TryGetValue(roomName, out rd))
			return null;

		// TODO: reuse rooms here
		GameObject roomGO = (GameObject)GameObject.Instantiate(rd.roomTemplate.gameObject);
		Room room = roomGO.GetComponent<Room>(); 
		rd.instantiatedRooms.Add(room);

		Transform roomTransform = room.transform;
		roomTransform.SetParent(m_RoomsRoot.transform);

		return room;
	}

	void LinkRooms(Room room1, Connector connector1, Room room2, Connector connector2)
	{
		Vector3 p1 = connector1.transform.position;
		Vector3 p2 = connector2.transform.position;

		Vector3 d2 = room2.transform.position - p2;
		room2.transform.position = p1 + d2;
	}

	void BuildRoomsChain(string prevRoomName, Room room)
	{
		string roomName = room.gameObject.name;
		room.gameObject.SetActive(true);
		room.SetRoomActive(false);
		foreach(var connector in room.m_Connectors)
		{
			if (prevRoomName != null && prevRoomName.StartsWith(connector.m_RoomName))
				continue;

			Room r = GetRoomInstance(connector.m_RoomName);
			if (r == null)
				continue;

			Connector c = r.GetConnector(connector.m_ConnectorIndex);
			LinkRooms(room, connector, r, c);

			if (roomName.StartsWith(connector.m_RoomName))
			{
				// Looping
				connector.m_Room = room;
				c.m_Room = room;
				r.gameObject.SetActive(true);
			}
			else
			{
				connector.m_Room = r;
				c.m_Room = room;
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
		m_CurrentRoom = GetRoomInstance(s_RoomNames[0]);
		BuildRoomsChain(null, m_CurrentRoom);

		//First room special treatment, or AKA hack.
		m_CurrentRoom.EnableEntryDoorHack();

		// Move player to the first room's anchor
		var firstRoomTransform = m_CurrentRoom.transform;
		var playerAnchorTransform = firstRoomTransform.Find("Anchors/Player");
		m_Player.transform.position = playerAnchorTransform.position;
		m_Player.transform.rotation = playerAnchorTransform.rotation;
		// Activate player
		m_CurrentRoom.SetRoomActive(true);
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
		Debug.Log ("Transition: " + room.name + " -> " + connector.m_Room.name);

		m_CurrentRoom.SetRoomActive(false);
		if (m_CurrentRoom == connector.m_Room)
		{
			// Looping
			Connector connectorTo = connector.m_Room.GetConnector(connector.m_ConnectorIndex);
			Vector3 d = m_Player.transform.position - connector.transform.position;
			m_Player.transform.position = connectorTo.transform.position + d;
			room.CloseDoorWithConnector(connector, false);
		}
		else
		{
			room.CloseDoorWithConnector(connector, true);
		}

		m_CurrentRoom = connector.m_Room;
		m_CurrentRoom.SetRoomActive(true);
	}
}
