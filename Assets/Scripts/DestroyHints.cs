using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHints : MonoBehaviour
{
    [SerializeField] private GameObject[] _hints;

    public void DestroyAll()
    {
        for (int i = 0; i < _hints.Length; ++i)
        {
            Destroy(_hints[i]);
        }
    }
}
