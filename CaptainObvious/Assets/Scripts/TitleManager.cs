using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour 
{
	public AudioClip introSoundBit;
	public AudioSource audioSource;
	public GameObject backGroundMusic;
	public Button titleButton;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(backGroundMusic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTitleClicked()
	{
		titleButton.interactable = false;
		
		if (introSoundBit == null)
		{
			OnAudioFinished();
			return;
		}

		audioSource.clip = introSoundBit;
		audioSource.Play();
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
		Application.LoadLevelAdditive("MainScene");
	}
}
