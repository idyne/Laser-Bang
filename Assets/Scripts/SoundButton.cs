using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Button _button;
    private Image _image;
    [SerializeField]
    private Sprite _on;
    [SerializeField]
    private Sprite _off;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _button.onClick.AddListener(Toggle);
        if (PlayerPrefs.GetInt("sound") == 0)
            _image.sprite = _off;
    }

    public void Toggle()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            PlayerPrefs.SetInt("sound", 1);
            _image.sprite = _on;
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
            _image.sprite = _off;
        }
    }
}
