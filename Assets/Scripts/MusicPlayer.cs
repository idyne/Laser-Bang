using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("music") == 0)
            Mute();
    }

    public void Mute()
    {
        _audio.volume = 0;
    }

    public void Unmute()
    {
        _audio.volume = 0.08f;
    }


}
