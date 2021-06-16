using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop(Transform target)
    {
        _rigidbody.AddForceAtPosition((transform.position - target.position) * 5, new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(0.1f, 0.5f),0), ForceMode.Impulse);
    }
}
