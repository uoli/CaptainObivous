using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Connector> m_Connectors = new List<Connector>();
	bool m_PrevLooping = false;
	public bool m_Looping = false;
	private MainNaration mainNarration;

	class LoopingData
	{
		public LoopingData(string _roomName, int _roomConnector, string _nextRoomName, int _nextRoomConnector)
		{
			roomName = _roomName;
			roomConnector = _roomConnector;
			nextRoomName = _nextRoomName;
			nextRoomConnector = _nextRoomConnector;
		}

		public string roomName;
		public int roomConnector;
		public string nextRoomName;
		public int nextRoomConnector;
	}
	private LoopingData[] s_LoopingData = new LoopingData[]
	{
		new LoopingData("Anger", 1, "Bargaining", 0),
		new LoopingData("Bargaining", 1, "Depression", 0),
		new LoopingData("Depression", 1, "Acceptance", 0)
	};
	LoopingData m_LoopingData = null;

	public Connector GetConnector(int index)
	{
		return m_Connectors[index];
	}

	void Start()
	{
		foreach (var ld in s_LoopingData)
		{
			if (name.StartsWith(ld.roomName))
				m_LoopingData = ld;
		}
	}

	// Use this for initialization
	void OnEnable () 
	{
		mainNarration = FindObjectOfType<MainNaration>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Looping != m_PrevLooping)
		{
			m_PrevLooping = m_Looping;
			Unloop();
		}
	}

	public void OnEnterLoop ()
	{
		mainNarration.gameObject.SetActive(true);
	}

	public void Unloop()
	{
		Connector connector = GetConnector(m_LoopingData.roomConnector);
		string nextRoom;
		int nextConnector;
		if (false)
		{
			nextRoom = m_LoopingData.roomName;
			nextConnector = m_LoopingData.roomConnector;
		}
		else
		{
			nextRoom = m_LoopingData.nextRoomName;
			nextConnector = m_LoopingData.nextRoomConnector;
		}
		
		RoomsController.GetInstance().RebuildRoomsChain(this, connector, nextRoom, nextConnector);
	}
	
	private void ResetPhone()
	{
		var phone = GameObject.Find("Phone");
		if (phone != null)
		{
			var phoneComponent = phone.GetComponent<PhoneNew>();
			phoneComponent.ResetDialedNumber();
		}
	}

	public void SetRoomActive(bool active)
	{
//		RoomExitTrigger[] triggers = transform.GetComponentsInChildren<RoomExitTrigger>();
//		foreach (var trigger in triggers)
//		{
//			trigger.enabled = active;
//		}
	}

	public void CloseDoorWithConnector(Connector connector, bool seal)
	{
		DoorAnimation doorAnim = connector.transform.parent.GetComponentInChildren<DoorAnimation>();
		if (doorAnim != null)
			doorAnim.Close(seal);
	}

	public void CloseDoorWithConnectorForLooping (Connector connector)
	{
		DoorAnimation doorAnim = connector.transform.parent.GetComponentInChildren<DoorAnimation>();
		if (doorAnim != null)
			doorAnim.CloseInstant(false);
	}

	public void EnableEntryDoorHack ()
	{
		var connector = GetConnector(1);
		var doorParent = connector.transform.parent;
		doorParent.GetChild(0).gameObject.SetActive(true);
		CloseDoorWithConnector(connector, true);
	}
}
