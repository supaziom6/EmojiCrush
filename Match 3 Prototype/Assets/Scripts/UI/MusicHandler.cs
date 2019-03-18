using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler: MonoBehaviour {

    /// Stores instances for music adn sound variables but is only incharge of managing music volume
    private static MusicHandler INSTANCE;
    public static bool MusicMuted;
    public static bool SoundMuted;
    public static float Volume = 1;
    public AudioSource Audio;
    
    // Use this for initialization
    void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;

            Audio.volume = Volume;
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

    }
}
