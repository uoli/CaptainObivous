using UnityEngine;
using System.Collections;

public class RoomExitTrigger : MonoBehaviour
{
	Room m_Room;
	Connector m_Connector;
	
	void OnEnable()
	{
		m_Room = gameObject.GetComponentInParent<Room>();
		m_Connector = gameObject.transform.parent.GetComponentInChildren<Connector>();
	}

	void OnTriggerEnter (Collider collider)
	{
		if (!enabled)
			return;

		RoomsController.GetInstance().OnRoomExit(m_Room, m_Connector);	
	}

	void OnTriggerExit (Collider collider)
	{

	}
}
