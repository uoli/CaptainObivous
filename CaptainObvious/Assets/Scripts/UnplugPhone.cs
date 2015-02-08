using UnityEngine;
using System.Collections;

public class UnplugPhone : MonoBehaviour
{
	public Room room;
	public GameObject go1ToHide;
	public GameObject go2ToHide;
	public GameObject go1ToShow;
	public GameObject go2ToShow;

	public void OnMouseDown()
	{
		if (Camera.main.audio.isPlaying)
			return;

		room.Unloop();

		go1ToHide.SetActive(false);
		go2ToHide.SetActive(false);
		go1ToShow.SetActive(true);
		go2ToShow.SetActive(true);
	}
}
