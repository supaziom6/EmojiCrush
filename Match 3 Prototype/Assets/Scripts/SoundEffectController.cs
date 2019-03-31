using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour {

	private AudioSource soundEffect;

	// Use this for initialization
	void Start () {
		soundEffect = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!MusicHandler.SoundMuted)
		{
			soundEffect.volume = MusicHandler.Volume;
		}
		else{
			soundEffect.volume = 0;
		}
	}
}
