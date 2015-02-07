using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomsController : MonoBehaviour
{
	// Player's FPS object 
	public GameObject m_Player;

	// Root object
	GameObject m_RoomsRoot;
	List<Room> m_Rooms = new List<Room>();
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

	void ConnectRooms(Room room1, int connector1, Room room2, int connector2)
	{
		Transform t1 = room1.GetConnector(connector1);
		Transform t2 = room2.GetConnector(connector2);

		Vector3 p1 = t1.position;
		Vector3 p2 = t2.position;

		Vector3 d2 = room2.transform.position - p2;
		room2.transform.position = p1 + d2;
	}

	void SetupRooms()
	{
		// Attach rooms to the root
		foreach(var roomName in s_RoomNames)
		{
			Room room = GameObject.Find(roomName).GetComponent<Room>();
			if (room == null)
				continue;

			m_Rooms.Add(room);
			Transform roomTransform = room.transform;
			roomTransform.SetParent (m_RoomsRoot.transform);
		}

		// Connect rooms
		ConnectRooms(m_Rooms[0], 0, m_Rooms[1], 1);

		// Move player to the first room's anchor
		var firstRoomTransform = transform.Find("Room1");
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
