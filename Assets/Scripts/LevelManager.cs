using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour

{
    public int _maxLevel = 100;
    [HideInInspector]
    public int _currentLevel;
    [SerializeField] Text _levelText;
    private Levels _levels;







    private void Awake()
    {
        _levels = FindObjectOfType<Levels>();
        PlayerPrefs.SetInt("init", 0);
        UpdateLevels();
        bool init = PlayerPrefs.GetInt("init") == 1;
        if (!init)
        {
            PlayerPrefs.DeleteAll();
            SetCurrentLevel(1);
            PlayerPrefs.SetInt("totalDiamonds", 0);
            PlayerPrefs.SetInt("skinBought_0", 1);
            PlayerPrefs.SetInt("sound", 1);
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.SetInt("init", 1);
        }
    }

    private void Start()
    {
        _levelText.text = "Level " + _currentLevel;
        //_levels.DestroyPreviousLevels();
        //_levels.DeactivateNextLevels();
    }



    public void CompleteLevel()
    {
        //PlayerPrefs.SetInt("totalDiamonds", PlayerPrefs.GetInt("totalDiamonds") + PlayerPrefs.GetInt("diamonds"));
        //PlayerPrefs.SetInt("diamonds", 0);
        FindObjectOfType<Diamond>().SetDiamonds();
        SetCurrentLevel(_currentLevel + 1);
    }


    public void SetCurrentLevel(int currentLevel)
    {
        if (currentLevel > _maxLevel)
            _currentLevel = _maxLevel;
        else
            _currentLevel = currentLevel;
        PlayerPrefs.SetInt("currentLevel", _currentLevel);
    }


    private void UpdateLevels()
    {
        _currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (_currentLevel == 0)
            SetCurrentLevel(1);
    }
}
