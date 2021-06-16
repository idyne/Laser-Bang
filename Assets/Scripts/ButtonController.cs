using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private LevelManager _levelManager;
    private AudioSource _audio;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioClip _sound;

    private void Awake()
    {
        //_settings = FindObjectOfType<SettingsButton>();
        gameObject.AddComponent<AudioSource>();
        _audio = GetComponent<AudioSource>();
        _audio.clip = _sound;
        _audio.volume = 0.3f;
        _audio.playOnAwake = false;
        _audio.enabled = true;
    }
    private void Start()
    {
        //_levelManager = LevelManager.LM;
        //SetOnClickListeners();
        
    }

    /*private void SetOnClickListeners()
    {
        if (_levelManager)
        {
            if (_reloadButton)
                _reloadButton.onClick.AddListener(_levelManager.LoadCurrentLevel);
            if (_mainMenuButton)
                _mainMenuButton.onClick.AddListener(_levelManager.LoadMainMenu);
            if (_nextLevelButton)
                _nextLevelButton.onClick.AddListener(_levelManager.LoadNextLevel);
            if (_playButton)
                _playButton.onClick.AddListener(_levelManager.LoadCurrentLevel);
            if (_homeButton)
                _homeButton.onClick.AddListener(_levelManager.LoadMainMenu);
        }
        if (_settingsButton)
            _settingsButton.onClick.AddListener(OpenSettings);
        if (_backButton)
            _backButton.onClick.AddListener(CloseSettings);


    }*/

    /*private void OpenSettings()
    {
        if (_settings._isClicked)
        {
            CloseSettings();
            return;
        }
        _settings._isClicked = true;
        _settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseSettings()
    {
        _settings._isClicked = false;
        _settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }*/

    public void PlaySound()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
            _audio.Play();
    }





}
