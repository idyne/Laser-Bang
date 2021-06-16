using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirrors : MonoBehaviour
{
    [HideInInspector] Mirror[] _mirrors;
    private Transform target;

    private void Awake()
    {
        _mirrors = transform.GetComponentsInChildren<Mirror>();
        target = transform.parent.Find("Target");
    }

    public void DropMirrors()
    {
        foreach(Mirror mirror in _mirrors)
        {
            mirror.Drop(target);
        }
    }
}
