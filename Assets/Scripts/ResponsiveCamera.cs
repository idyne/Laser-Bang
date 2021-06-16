using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{

    private float _screenRatio;
    private Camera _mainCamera;
    private void Awake()
    {
        _screenRatio = (float)Screen.width / (float)Screen.height;
        _mainCamera = Camera.main;
        if (_screenRatio > 9f / 16)
            _mainCamera.orthographicSize = 5;
        else
            _mainCamera.orthographicSize = (5f / (16f / 9)) * (1 / _screenRatio);
    }

}
