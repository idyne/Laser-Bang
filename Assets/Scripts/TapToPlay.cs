using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    private Button _button;
    [SerializeField]
    private GameObject _playButton;
    private static bool _init = false;
    private Animator _animator;

    private void Awake()
    {
        if (_init)
        {
            gameObject.SetActive(false);
            return;
        }
        else
            _init = true;
        _playButton.SetActive(false);
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Play);
    }

    private void Play()
    {
        StartCoroutine(_Play());
    }

    private IEnumerator _Play()
    {
        _animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        _playButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
