using UnityEngine;
using System.Collections;

public class RoomsController : MonoBehaviour
{
	// Player's FPS object 
	public GameObject m_Player;

	// Root object
	GameObject m_RoomsRoot;
	int m_RoomsLoaded;
	static string[] s_RoomNames = new string[]{"Room1"};
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

	void SetupRooms()
	{
		foreach(var roomName in s_RoomNames)
		{
			Transform roomTransform = GameObject.Find(roomName).transform;
			roomTransform.SetParent (m_RoomsRoot.transform);
		}

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
