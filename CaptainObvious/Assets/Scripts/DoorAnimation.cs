using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour
{
	float time = 0;

	void OnEnable()
	{
		time = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		float angle = Mathf.LerpAngle(0, -90, time);
		transform.localEulerAngles = new Vector3(0, angle, 0);
		if (time >= 1.0f)
		{
			enabled = false;
		}
	}

	public void Reset()
	{
		transform.localEulerAngles = new Vector3(0, 0, 0);
	}
}
