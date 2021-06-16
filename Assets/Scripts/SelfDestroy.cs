using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float delay;
    void Start()
    {
        StartCoroutine(DestroyMe());
    }

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
