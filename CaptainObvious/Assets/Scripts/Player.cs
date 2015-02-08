using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public CameraShaker camShaker;
	public AudioClip rageMessage;

	float rageMeter = 0;
	float rageIncreaseCoolDown = 0;
	float maxRageCooldown = 2f;
	float rageDecreaseStep = .5f;
	bool playedRageMessage = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		TickDecayRage();
	}

	void TickDecayRage()
	{
		if (rageIncreaseCoolDown > 0)
		{
			rageIncreaseCoolDown -=  Time.deltaTime;
			rageIncreaseCoolDown = Mathf.Max(rageIncreaseCoolDown, 0);
		}
		
		if (rageIncreaseCoolDown <= 0 && rageMeter > 0)
		{
			rageMeter -= rageDecreaseStep * Time.deltaTime;
			rageMeter = Mathf.Max(rageMeter, 0);
			camShaker.intensity = rageMeter;
		}
	}

	public void IncreaseRage()
	{
		rageMeter += 0.2f;
		rageMeter = Mathf.Min(rageMeter, 1f);
		rageIncreaseCoolDown = maxRageCooldown;
		camShaker.intensity = rageMeter;
		if (!playedRageMessage && rageMeter > 0.7f)
		{
			Camera.main.audio.clip = rageMessage;
			Camera.main.audio.Play();
		}
	}
}
