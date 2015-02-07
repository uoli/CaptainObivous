using UnityEngine;
using System.Collections;

public class MainNaration : MonoBehaviour {
	public AudioClip[] narationLines;
	private int loopCount = 0;

	void OnTriggerEnter (Collider col)
	{
		if (loopCount < narationLines.Length)
		{
			audio.PlayOneShot(narationLines[loopCount]);
			loopCount++;
			this.enabled = false;
		}
	}
}
