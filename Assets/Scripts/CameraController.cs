using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private LevelManager _levelManager;
    private Vector3 _desiredPosition;
    private Controller _controller;
    public bool _isSet = true;
    [SerializeField]
    private Text _levelText;
    private Levels _levels;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _controller = FindObjectOfType<Controller>();
        _levels = FindObjectOfType<Levels>();
    }

    private void Start()
    {
        SetDesiredPosition();
        _isSet = true;
        transform.position = _desiredPosition;
        Camera.main.fieldOfView = FieldOfView(((float)Screen.width) / Screen.height);
    }

    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, 0.05f);
        transform.position = smoothedPosition;
        if (!_isSet && Vector3.Distance(transform.position, _desiredPosition) < 0.1f)
        {
            _isSet = true;
            _controller._menu.SetActive(true);
            _controller._isLaserSend = false;
            _controller._previewSparks.SetActive(true);
            _levelText.text = "Level " + _levelManager._currentLevel;
            _levels.DestroyPreviousLevel();
        }

    }

    public void SetDesiredPosition()
    {
        _desiredPosition = new Vector3(transform.position.x, transform.position.y, (_levelManager._currentLevel - 1) * 30 - 6.5f);
        _isSet = false;
    }

    private float FieldOfView(float x)
    {
        float result = 105 - 80 * x;
        if (result > 70)
            result = 70;
        else if (result < 60)
            result = 60;
        return result;
    }
}
