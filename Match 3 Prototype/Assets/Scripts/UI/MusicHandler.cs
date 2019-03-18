using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler: MonoBehaviour {

    private static MusicHandler INSTANCE;
    public static float Volume = 1;
    public AudioSource Audio;
    public AudioSource Sound;
    public static bool MusicMuted;
    public static bool SoundMuted;
    // Use this for initialization
    void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;

            Audio.volume = Volume;
            Sound.volume = Volume;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }
    private void Update()
    {
        if(MusicMuted)
        {
            Audio.volume = 0;
        }
        else
        {
            Audio.volume = Volume;
        }
        if(SoundMuted)
        {
            Sound.volume = 0;
        }
        else
        {
            Sound.volume = Volume;
        }

    }
}
