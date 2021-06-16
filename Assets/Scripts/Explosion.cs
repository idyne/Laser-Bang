using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMe());
    }

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
