using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Transform> m_Connectors = new List<Transform>();

	public Transform GetConnector(int index)
	{
		return m_Connectors[index];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
