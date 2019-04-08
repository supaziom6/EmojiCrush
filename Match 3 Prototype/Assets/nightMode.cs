using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightMode : MonoBehaviour {
	public GameObject[] emojiGlows;
	public GameObject darkener;

	private bool NightModeOn = true;
	
	// Update is called once per frame
	public void toggleNightMode()
	{
		NightModeOn = !NightModeOn;
		foreach(GameObject g in emojiGlows)
		{
			g.SetActive(NightModeOn);
		}
		darkener.SetActive(NightModeOn);

	}
}
