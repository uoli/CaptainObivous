using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomsController : MonoBehaviour
{
	// Player's FPS object 
	public GameObject m_Player;

	// Root object
	GameObject m_RoomsRoot;
	Dictionary<string, Room> m_Rooms = new Dictionary<string, Room>();
	List<Room> m_RoomsCopy = new List<Room>();
	int m_RoomsLoaded;
	static string[] s_RoomNames = new string[]{"Room1", "Room2"};
	const int kLevelLoadUpdateCount = 2;


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

	void AlignRooms(Room room1, Connector connector1, Room room2, Connector connector2)
	{
		Vector3 p1 = connector1.transform.position;
		Vector3 p2 = connector2.transform.position;

		Vector3 d2 = room2.transform.position - p2;
		room2.transform.position = p1 + d2;
	}

	void BuildRoomsChain(Room startRoom)
	{
		startRoom.gameObject.SetActive(true);
		foreach(var connector in startRoom.m_Connectors)
		{
			Room r = m_Rooms[connector.m_RoomName];
			Connector c = r.GetConnector(connector.m_ConnectorIndex);

			if (r != startRoom)
			{
				AlignRooms(startRoom, connector, r, c);
				r.gameObject.SetActive(true);
			}
			else
			{
				// Looping

			}
		}
	}

	void SetupRooms()
	{
		// Attach rooms to the root
		foreach(var roomName in s_RoomNames)
		{
			Room room = GameObject.Find(roomName).GetComponent<Room>();
			if (room == null)
				continue;

			m_Rooms.Add(roomName, room);
			Transform roomTransform = room.transform;
			roomTransform.SetParent (m_RoomsRoot.transform);
			room.gameObject.SetActive(false);

			GameObject roomCopy = (GameObject)GameObject.Instantiate(room.gameObject);
			m_RoomsCopy.Add(roomCopy.GetComponent<Room>());
		}

		// Connect rooms
		BuildRoomsChain(m_Rooms["Room1"]);

		// Move player to the first room's anchor
		var firstRoomTransform = m_Rooms["Room1"].transform;
		var playerAnchorTransform = firstRoomTransform.Find("Anchors/Player");
		m_Player.transform.position = playerAnchorTransform.position;
		// Activate player
		m_Player.SetActive(true);
	}

	// Use this for initialization
	void Start()
	{
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
}
