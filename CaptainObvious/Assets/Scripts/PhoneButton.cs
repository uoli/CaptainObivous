using UnityEngine;
using System.Collections;

public class PhoneButton : MonoBehaviour
{
	public int digit;
	public AudioClip goodSound;
	public AudioClip badSound;

	public void OnMouseDown()
	{
		var audioSource = Camera.main.audio;
		if (audioSource.isPlaying)
			return;
			
		var phone = GameObject.Find("Phone");
		var phoneComponent = phone.GetComponent<PhoneNew>();
		if (phoneComponent.DialDigit((char) ('0' + digit)))
			audioSource.clip = goodSound;
		else
			audioSource.clip = badSound;
		audioSource.Play();
	}
}
