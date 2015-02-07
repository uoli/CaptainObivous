using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static private GameManager s_GameManager = null;
	private bool m_Looping = true;
	private Room m_CurrentRoom;
	RoomsController roomController;

	public Player m_Player;

	public static GameManager instance
	{
		get { return s_GameManager; }
	}

	bool Looping {
		get {return m_Looping;}
		set {
			if (m_Looping != value)
			{
				m_Looping = value;
				OnLoopingChanged();
			}
		}
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

	void OnLoopingChanged()
	{
		if (m_Looping)
			roomController.ConnectRooms(m_CurrentRoom, 0, m_CurrentRoom, 1);
		else 
		{
			roomController.ConnectNextRoom(m_CurrentRoom);
		}
	}

	void OnGUI()
	{
		Looping = GUILayout.Toggle(Looping,"Looping");
	}

	public void OnRoomExit(Room room)
	{
		if (!Looping)
			return;

		Transform connector1 = room.GetConnector(0);
		Transform connector2 = room.GetConnector(1);
		Vector3 d = m_Player.transform.position - connector1.position;
		m_Player.transform.position = connector2.position + d;
	}
}
