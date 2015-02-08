using UnityEngine;
using System.Collections;

public class SpawnFPSIfNone : MonoBehaviour
{
	public GameObject FPSController;

	void Start () 
	{
		var fpsController = GameObject.Find("First Person Controller");
		if ( fpsController == null )
		{
			fpsController = (GameObject) Instantiate(FPSController);	
			fpsController.transform.position += new Vector3(0, 1f, 0);
		}
	}
}
