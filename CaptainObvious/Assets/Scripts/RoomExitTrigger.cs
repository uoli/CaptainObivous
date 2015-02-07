using UnityEngine;
using System.Collections;

public class RoomExitTrigger : MonoBehaviour
{
	Room m_Room;
	void OnEnable()
	{
		m_Room = gameObject.GetComponentInParent<Room>();
	}

	void OnTriggerEnter (Collider collider)
	{
		GameManager.instance.OnRoomExit(m_Room);	
	}
}
