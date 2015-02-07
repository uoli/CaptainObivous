using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

	[Range(0,1)]
	public float intensity = 0;
	public int maxRange = 0;
	public Camera cam;

	private Vector3 lastDelta;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (intensity <= 0)
			return;

		var maxVal = maxRange * 0.5f * intensity;
		var deltax = Random.Range(-maxVal,maxVal);
		var deltay = Random.Range(-maxVal,maxVal);
		var deltaz = Random.Range(-maxVal,maxVal);
		
		var delta = new Vector3(deltax, deltay, deltaz);
		cam.transform.localEulerAngles += delta;
	}
}
