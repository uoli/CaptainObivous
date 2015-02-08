using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public UIPhone phoneUI;
	public AudioSource audioSource;
	public AudioClip soundBite;

	private Player m_Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Interact (Player player)
	{
		m_Player = player;
		OpenUI();
	}

	void OpenUI ()
	{
		var mouseLook = m_Player.GetComponent<MouseLook>();
		var fpsController = m_Player.GetComponent<FPSInputController>();
		
		phoneUI.gameObject.SetActive(true);
		mouseLook.enabled = false;
		fpsController.enabled = false;
	}

	public void Close ()
	{
		
		var mouseLook = m_Player.GetComponent<MouseLook>();
		var fpsController = m_Player.GetComponent<FPSInputController>();
		
		phoneUI.gameObject.SetActive(false);
		mouseLook.enabled = true;
		fpsController.enabled = true;

		if(audioSource.clip == soundBite)
		{
			audioSource.Stop();
		}

	}

	public void CallCorrectNumber ()
	{
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
		while(audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}
		OnCallFinished();
	}
}
