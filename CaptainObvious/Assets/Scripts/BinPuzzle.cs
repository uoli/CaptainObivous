using UnityEngine;
using System.Collections;

public class BinPuzzle : MonoBehaviour {
	public AudioClip firstMessage;
	public AudioClip secondMessage;
	private int numberOfNotesInBin = 0;

	void OnCollisionEnter(Collision col)
	{
		if (col.other.gameObject.name.Equals("CrushedPaperNote"))
			numberOfNotesInBin++;
		if (numberOfNotesInBin == 1)
		{
			Camera.main.audio.clip = firstMessage;
			Camera.main.audio.Play();
		}
		if (numberOfNotesInBin == 3 && secondMessage != null)
		{
			Camera.main.audio.clip = secondMessage;
			Camera.main.audio.Play();
			Room room = gameObject.transform.root.GetComponent<Room>();
			if (room != null)
			{
				room.Unloop();
			}
		}
	}
}
