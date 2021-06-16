using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private AudioSource _audio;
    private Button _button;
    [SerializeField]
    private AudioClip _sound;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        _audio = GetComponent<AudioSource>();
        _audio.clip = _sound;
        _audio.volume = 0.3f;
        _audio.playOnAwake = false;
        _audio.enabled = true;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlaySound);
    }
    
    private void PlaySound()
    {
        if(PlayerPrefs.GetInt("sound") == 1)
            _audio.Play();
    }


}
