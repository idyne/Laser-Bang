using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale0to1 : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private float _time;
    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    void Start()
    {
        _initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        Scale();
    }

    void Scale()
    {
        LeanTween.scale(gameObject, Vector3.one * _multiplier, _time);
    }
}
