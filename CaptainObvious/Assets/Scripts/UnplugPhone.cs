using UnityEngine;
using System.Collections;

public class UnplugPhone : MonoBehaviour
{
	public Room room;

	public void OnMouseDown()
	{
		if (Camera.main.audio.isPlaying)
			return;

		room.Unloop();
	}
}
