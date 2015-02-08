using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour 
{
	public AudioSource audioSource;
	public GameObject backGroundMusic;
	public Button titleButton;
	public Animator animator;

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
		animator.SetTrigger("StartAnim");
		

		StartCoroutine(WaitForCallToFinish());
	}

	IEnumerator WaitForCallToFinish()
	{

		yield return new WaitForSeconds(1.5f);

		OnTitleFinished();
	}

	void OnTitleFinished ()
	{
		Application.LoadLevelAdditive("MainScene");
	}
}
