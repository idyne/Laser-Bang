using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCameraController : MonoBehaviour
{
    private LevelManager _levelManager;
    [SerializeField]
    private float _offset;

    private float _minPosition;
    [SerializeField]
    private float _speed = 0.1f;
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _level1;
    private float _maxPosition;

    void Start()
    {
        //_levelManager = LevelManager.LM;
        transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("Level " + PlayerPrefs.GetInt("lastPlayed")).transform.position.z + _offset);
        Time.timeScale = 1;
        _minPosition = _level1.position.z + _offset;
        _maxPosition = _target.position.z - Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(0, -touchDeltaPosition.y * _speed, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 10 * _speed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -10 * _speed, 0);
        }
        
        if (transform.position.z < _minPosition)
            transform.position = new Vector3(transform.position.x, transform.position.y, _minPosition);
        if (transform.position.z > _maxPosition)
            transform.position = new Vector3(transform.position.x, transform.position.y, _maxPosition);
    }


}
