using UnityEngine;
using System.Collections;

public class BinPuzzle : MonoBehaviour {
	public int numberOfNotesRequired;
	private int numberOfNotesInBin;

	void OnCollisionEnter(Collision col)
	{
		if (col.other.gameObject.name.Equals("CrushedPaperNote"))
			numberOfNotesInBin++;
		Debug.Log(numberOfNotesInBin);
	}
}
