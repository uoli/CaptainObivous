using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour 
{
	public AudioClip introSoundBit;
	public AudioSource audioSource;
	public Button titleButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTitleClicked()
	{
		audioSource.clip = introSoundBit;
		audioSource.Play();
		titleButton.interactable = false;
		StartCoroutine(WaitForCallToFinish());
	}

	IEnumerator WaitForCallToFinish()
	{
		while(audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}
		OnAudioFinished();
	}

	void OnAudioFinished ()
	{
		Application.LoadLevel("MainScene");
	}
}
