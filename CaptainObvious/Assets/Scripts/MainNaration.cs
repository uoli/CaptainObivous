using UnityEngine;
using System.Collections;

public class MainNaration : MonoBehaviour {
	public AudioClip[] narationLines;
	private int loopCount = 0;

	void OnTriggerEnter (Collider col)
	{
		if (loopCount >= narationLines.Length)
			return;
		
		Camera.main.audio.clip = narationLines[loopCount];
		Debug.Log(narationLines[loopCount]);
		Camera.main.audio.Play();
		loopCount++;
		gameObject.SetActive(false);

	}

	void SkipCurrentNarration()
	{
		Camera.main.audio.Stop();
	}
}
