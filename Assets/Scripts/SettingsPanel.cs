using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Switch _soundSwitch;
    [SerializeField] private Switch _musicSwitch;
    void Start()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
            _soundSwitch.Toggle();
        if (PlayerPrefs.GetInt("music") == 0)
            _musicSwitch.Toggle();
    }

}
