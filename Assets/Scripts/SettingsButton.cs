using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _menu;
    public void On()
    {
        _menu.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void Off()
    {
        _menu.SetActive(true);
        _settingsPanel.SetActive(false);
    }
}
