using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private Button _button;

    private Animator _animator;

    private bool _on = true;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _button.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        if (_on)
        {
            _animator.SetTrigger("Off");
            _on = false;
        }
        else
        {
            _animator.SetTrigger("On");
            _on = true;
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("sound") == 1)

            PlayerPrefs.SetInt("sound", 0);
        else
            PlayerPrefs.SetInt("sound", 1);
    }

    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            FindObjectOfType<MusicPlayer>().Mute();
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            FindObjectOfType<MusicPlayer>().Unmute();
        }

    }
}
