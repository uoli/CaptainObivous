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

}
