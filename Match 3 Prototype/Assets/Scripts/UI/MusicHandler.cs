using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler: MonoBehaviour {

    public static float Volume = 1;
    private bool MusicToggle;
    public Slider slider;
    public Toggle Music;
    public AudioSource Audio;
    // Use this for initialization
    void Awake()
    {
        MusicToggle = true;
        slider.value = Volume;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Music"));
    }
    public void OnValueChanged()
    {
        Audio.GetComponent<AudioSource>().volume = slider.value;
        Volume = slider.value;
    }
    public void ToggleMusic()
    {
        if(MusicToggle)
        {
            Audio.mute = true;
        }
        if(!MusicToggle)
        {
            Audio.mute = false;
        }
    }
}
