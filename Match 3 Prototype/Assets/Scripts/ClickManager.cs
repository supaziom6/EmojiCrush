using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

	private static ClickManager Instance;
	public GameObject clickPrefab;

	public Color[] potentialClolors;
	void Awake()
	{
		if(Instance == null)
		{
			DontDestroyOnLoad(this);
			Instance = this;
			if(potentialClolors == null)
			{
				potentialClolors = new Color[1];
				potentialClolors[0] = clickPrefab.GetComponent<SpriteRenderer>().color;
			}
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
			GameObject temp = Instantiate(clickPrefab, spawnLocation, Quaternion.identity);
			temp.GetComponent<SpriteRenderer>().color = potentialClolors[Random.Range(0,potentialClolors.Length)];
		}
	}
}
