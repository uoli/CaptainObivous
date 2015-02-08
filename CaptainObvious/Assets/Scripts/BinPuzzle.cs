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
		if (numberOfNotesInBin == 1 && firstMessage != null)
		{
			Camera.main.audio.clip = firstMessage;
			Camera.main.audio.Play();
		}
		if (numberOfNotesInBin == 3 && secondMessage != null)
		{
			Camera.main.audio.clip = secondMessage;
			Camera.main.audio.Play();
		}
		Debug.Log (numberOfNotesInBin);
		if (numberOfNotesInBin >= 3)
		{
			Room room = gameObject.transform.root.gameObject.GetComponent<Room>();
			Debug.Log(room);
			if (room != null)
			{
				room.Unloop();
			}
		}
	}
}
