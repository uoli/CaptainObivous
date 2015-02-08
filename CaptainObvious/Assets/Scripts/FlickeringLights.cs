using UnityEngine;
using System.Collections;

public class FlickeringLights : MonoBehaviour {
	public float minFlickerSpeed = 2;
	public float maxFlickerSpeed = 6;

	// Use this for initialization
	void Start () {
	
	}
	

	
	void Update()
	{
		if (light.enabled == false) { 
			Invoke ("TurnOnLights", Random.Range (minFlickerSpeed, maxFlickerSpeed));
		}
			else {
			Invoke("TurnOffLights", Random.Range(minFlickerSpeed,maxFlickerSpeed));
			}

		}
	void TurnOnLights () {
		light.enabled = true;
	}

	void TurnOffLights () {
		light.enabled = false;
	}

    }
