using UnityEngine;
using System.Collections;

public class AnsweringMachine : MonoBehaviour
{
	public AudioClip audioClip;

	public void OnMouseDown()
	{
		var audioSource = Camera.main.audio;
		if ( !audioSource.isPlaying )
		{
			audioSource.clip = audioClip;
			audioSource.Play();
		}	
	}
}
