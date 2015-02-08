using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public UIPhone phoneUI;
	public AudioClip soundBite;

	private Player m_Player;
	private bool _isOpen;

	public void Interact (Player player)
	{
		m_Player = player;
		OpenUI();
	}

	public void OnMouseDown()
	{
		if (!_isOpen)
			OpenUI();
	}

	void OpenUI ()
	{
		var mouseLook = m_Player.GetComponent<MouseLook>();
		var fpsController = m_Player.GetComponent<FPSInputController>();
		
		phoneUI.gameObject.SetActive(true);
		mouseLook.enabled = false;
		fpsController.enabled = false;
		_isOpen = true;
	}

	public void Close ()
	{
		var mouseLook = m_Player.GetComponent<MouseLook>();
		var fpsController = m_Player.GetComponent<FPSInputController>();
		
		phoneUI.gameObject.SetActive(false);
		mouseLook.enabled = true;
		fpsController.enabled = true;

		var audioSource = Camera.main.audio;
		if(audioSource.clip == soundBite)
		{
			audioSource.Stop();
		}

		_isOpen = false;
	}

	public void CallCorrectNumber ()
	{
		var audioSource = Camera.main.audio;
		audioSource.clip = soundBite;
		audioSource.Play();
		StartCoroutine(WaitForCallToFinish());
	}

	void OnCallFinished() 
	{
		phoneUI.OnHangUp();
	}

	IEnumerator WaitForCallToFinish()
	{
		var audioSource = Camera.main.audio;
		while(audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}
		OnCallFinished();
	}
}
