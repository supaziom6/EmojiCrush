using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

	private static ClickManager Instance;
	public GameObject clickPrefab;
	void Awake()
	{
		if(Instance == null)
		{
			DontDestroyOnLoad(this);
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{		
			Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			spawnLocation.z = -5;
			Instantiate(clickPrefab, spawnLocation, Quaternion.identity);
		}
	}
}
