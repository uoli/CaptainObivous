using UnityEngine;
using System.Collections;

public class PhoneButton : MonoBehaviour
{
	public int digit;

	public void OnMouseDown()
	{
		if (Camera.main.audio.isPlaying)
			return;
			
		var phone = GameObject.Find("Phone");
		var phoneComponent = phone.GetComponent<PhoneNew>();
		phoneComponent.DialDigit((char) ('0' + digit));
	}
}
