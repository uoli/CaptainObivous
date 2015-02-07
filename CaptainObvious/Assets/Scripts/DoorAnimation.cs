using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

	float time = 0;
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		float angle = Mathf.LerpAngle(0, -90, time);
		transform.localEulerAngles = new Vector3(0, angle, 0);
	}
}
