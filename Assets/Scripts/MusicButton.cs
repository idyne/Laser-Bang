using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private MusicPlayer _musicPlayer;
    [SerializeField]
    private Sprite _on;
    [SerializeField]
    private Sprite _off;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _button.onClick.AddListener(Toggle);
        if (PlayerPrefs.GetInt("music") == 0)
            _image.sprite = _off;
    }

    public void Toggle()
    {
        if(PlayerPrefs.GetInt("music") == 0) {
            _musicPlayer.Unmute();
            PlayerPrefs.SetInt("music", 1);
            _image.sprite = _on;
        }
        else
        {
            _musicPlayer.Mute();
            PlayerPrefs.SetInt("music", 0);
            _image.sprite = _off;
        }
    }
}
