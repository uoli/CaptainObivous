using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static private GameManager s_GameManager = null;
	private Room m_CurrentRoom;
	RoomsController roomController;

	public Player m_Player;

	public static GameManager instance
	{
		get { return s_GameManager; }
	}

	// Use this for initialization
	void Start () 
	{
		if (s_GameManager == null)
			s_GameManager = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRoomExit(Room room, Connector connector)
	{
		if (!room.m_Looping)
			return;

		Connector connector1 = room.GetConnector(1);
		Connector connector2 = room.GetConnector(0);
		Vector3 d = m_Player.transform.position - connector1.transform.position;
		m_Player.transform.position = connector2.transform.position + d;
	}
}
