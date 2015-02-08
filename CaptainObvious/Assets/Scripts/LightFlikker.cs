using UnityEngine;
using System.Collections;

public class LightFlikker : MonoBehaviour {

	public float MinInterval = 0.7f;
	public float MaxInterval = 1.3f;

	public float MinStrength = .7f;
	public float MaxStrength = 1.2f;

	// Use this for initialization
	void Start () {
		Invoke("NewLight", Random.Range(MinInterval, MaxInterval));
	}

	void NewLight(){
		int i = Random.Range(0,4);
		if(i == 0){
			this.GetComponent<Light>().enabled = false;
		} else {
			this.GetComponent<Light>().enabled = true;
			this.GetComponent<Light>().intensity = Random.Range(MinStrength, MaxStrength);
		}

		Invoke("NewLight", Random.Range(MinInterval, MaxInterval));
	}
}
