using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private float _delay;
    private GameManager _gameManager;
    [SerializeField] private ParticleSystem _sparks;
    public bool _isCollected = false;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _delay = 0.3f;
    }
    public void Collect()
    {
        if (!_isCollected)
        {
            _isCollected = true;
            _gameManager.IncrementDiamonds();
            _sparks.Play();
            transform.Find("Diamond").gameObject.SetActive(false);
            StartCoroutine(DestroyMe());
        }
    }

    public IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(_delay);
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        _isCollected = false;
        transform.Find("Diamond").gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}
