using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Connector> m_Connectors = new List<Connector>();
	public bool m_Looping = false;
	private MainNaration mainNarration;

	public Connector GetConnector(int index)
	{
		return m_Connectors[index];
	}

	// Use this for initialization
	void OnEnable () 
	{
		mainNarration = FindObjectOfType<MainNaration>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnEnterLoop ()
	{
		mainNarration.gameObject.SetActive(true);
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
