using UnityEngine;
using System.Collections;

public class RoomsController : MonoBehaviour
{
	// Player's FPS object 
	public GameObject m_Player;
	// Root object
	GameObject m_RoomsRoot;

	void LoadRoom (string roomName)
	{
		Application.LoadLevelAdditiveAsync (roomName);

		Transform roomTransform = null;
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (obj.transform.parent == null && obj.name == roomName)
			{
				roomTransform = obj.transform;
				break;
			}
		}
		roomTransform.SetParent (m_RoomsRoot.transform);
	}

	// Use this for initialization
	void Start()
	{
		m_RoomsRoot = gameObject;


		// TODO: Move to loading screen
		// Load initial level layout
		LoadRoom ("Room1");
		LoadRoom ("Room2");

		// TODO: Connect rooms

		// Move player to the first room's anchor
		var firstRoomTransform = transform.Find("Rooms/Room1");
		var playerAnchorTransform = firstRoomTransform.Find("Anchors/Player");
		m_Player.transform.position = playerAnchorTransform.position;

		m_Player.SetActive (true);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
