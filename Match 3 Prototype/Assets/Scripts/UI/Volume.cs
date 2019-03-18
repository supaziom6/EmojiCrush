using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;
    public MusicHandler MH;
    public void OnValueChanged()
    {
        MH.Audio.volume = slider.value;
    }
    public void ToggleMusic(bool MusicToggled)
    {
        MusicHandler.MusicMuted = MusicToggled;
    }
    public void ToggleSoundEffects(bool SoundToggled)
    {
        MusicHandler.SoundMuted = SoundToggled;
    }
}
