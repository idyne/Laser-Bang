using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [HideInInspector] public Level[] _levels;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _levels = transform.GetComponentsInChildren<Level>();
    }

    public void DestroyPreviousLevels()
    {
        for (int i = 0; i < _levelManager._currentLevel - 1; ++i)
        {
            _levels[i].Destroy();
            _levels[i] = null;
        }
    }

    public void DeactivateNextLevels()
    {
        for (int i = _levelManager._currentLevel; i < _levelManager._maxLevel; ++i)
        {
            _levels[i].Deactivate();
        }
    }

    public void ActivateCurrentLevel()
    {
        _levels[_levelManager._currentLevel - 1].Activate();
    }

    public void DeactivatePreviousLevel()
    {
        _levels[_levelManager._currentLevel - 2].Deactivate();
    }

    public void DestroyPreviousLevel()
    {
        _levels[_levelManager._currentLevel - 2].Destroy();
        _levels[_levelManager._currentLevel - 2] = null;
    }
}
