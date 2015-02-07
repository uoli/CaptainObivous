using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Connector> m_Connectors = new List<Connector>();
	public bool m_Looping = false;

	public Connector GetConnector(int index)
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
