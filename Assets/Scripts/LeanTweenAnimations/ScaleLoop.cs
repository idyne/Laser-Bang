using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLoop : MonoBehaviour
{
    [SerializeField] private float _minMultiplier = 0.9f;
    [SerializeField] private float _maxMultiplier = 1;
    [SerializeField] private float _time = 1;
    private Vector3 _initialScale;


    private void Awake()
    {
        _initialScale = transform.localScale;
    }
    void Start()
    {
        Big();
    }

    void Small()
    {
        LeanTween.scale(gameObject, _initialScale * _minMultiplier, _time).setOnComplete(Big);
    }
    void Big()
    {
        LeanTween.scale(gameObject, _initialScale * _maxMultiplier, _time).setOnComplete(Small);
    }
}
