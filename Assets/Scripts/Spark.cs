using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{

    private float _lifetime = 0.4f;

    private void Awake()
    {
        _lifetime = GetComponent<ParticleSystem>().main.duration;
    }
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}
