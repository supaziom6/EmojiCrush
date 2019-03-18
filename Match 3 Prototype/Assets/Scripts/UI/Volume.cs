using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider.value =  MusicHandler.Volume;
    }
    public void OnValueChanged()
    {
        MusicHandler.Volume = slider.value;
    }
    public void ToggleMusic()
    {
        MusicHandler.MusicMuted = !MusicHandler.MusicMuted;
    }
    public void ToggleSoundEffects()
    {
        MusicHandler.SoundMuted = !MusicHandler.SoundMuted;
    }
}
