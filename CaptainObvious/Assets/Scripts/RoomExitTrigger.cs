using UnityEngine;
using System.Collections;

public class RoomExitTrigger : MonoBehaviour
{
	Room m_Room;
	Connector m_Connector;
	
	void OnEnable()
	{
		m_Room = gameObject.GetComponentInParent<Room>();
		m_Connector = gameObject.GetComponentInParent<Connector>();
	}

	void OnTriggerEnter (Collider collider)
	{
		RoomsController.instance.OnRoomExit(m_Room, m_Connector);	
	}
}
