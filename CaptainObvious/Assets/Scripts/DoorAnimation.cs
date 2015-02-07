using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour
{
	float m_Time = 0;
	bool m_Sealed = false;

	enum State
	{
		Idle,
		Opening,
		Closing
	}
	State m_State = State.Idle;


	// Update is called once per frame
	void Update ()
	{
		switch (m_State)
		{
		case State.Idle:
			return;
		case State.Opening:
			m_Time += Time.deltaTime;
			break;
		case State.Closing:
			m_Time -= Time.deltaTime;
			break;
		}

		float angle = Mathf.LerpAngle(0, -90, m_Time);
		transform.localEulerAngles = new Vector3(0, angle, 0);
		if (m_Time >= 1.0f || m_Time <= 0.0)
			m_State = State.Idle;
	}

	public void Open()
	{
		if (m_Sealed)
			return;

		enabled = true;
		m_Time = 0.0f;
		m_State = State.Opening;
	}

	public void Close(bool seal)
	{
		enabled = true;
		m_Time = 0.0f;
		if (!m_Sealed && seal)
			m_Sealed = true;
		m_State = State.Closing;
	}
}
