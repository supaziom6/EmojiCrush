using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler: MonoBehaviour {

    public static float Volume = 1;
    public Slider slider;
    public Toggle Music;
    public AudioSource Audio;
    public AudioSource Sound;
    // Use this for initialization
    void Awake()
    {
        slider.value = Volume;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Music"));
    }
    public void OnValueChanged()
    {
        Audio.volume = slider.value;
        Volume = slider.value;
    }
    public void ToggleMusic(bool MusicToggled)
    {
        if (MusicToggled == false)
        {
            Audio.volume = 0;
        }
        if(MusicToggled == true)
        {
            Audio.volume = slider.value;
        }
    }
    public void ToggleSoundEffects(bool SoundToggled)
    {
        if(SoundToggled == false)
        {
            Sound.volume = 0;
        }
        if (SoundToggled == true)
        {
            Sound.volume = slider.value;
        }
    }
}
