using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletePanel : MonoBehaviour
{
    public Text _text;
    private Levels _levels;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _confetti;
    [SerializeField]
    private Text _totalDiamondsText;
    [SerializeField]
    private Text _diamondText;
    [SerializeField]
    private GameObject _diamondUI;
    [SerializeField]
    private GameObject _tapToContinue;
    private bool _canGoNextLevel = false;

    private void Awake()
    {
        _levels = FindObjectOfType<Levels>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Animation()
    {
        _canGoNextLevel = false;
        _tapToContinue.SetActive(false);
        _diamondUI.SetActive(true);
        _diamondText.text = "+ " + PlayerPrefs.GetInt("diamonds").ToString();
        StartCoroutine(DiamondAnimation(PlayerPrefs.GetInt("diamonds"), PlayerPrefs.GetInt("totalDiamonds")));
        PlayerPrefs.SetInt("totalDiamonds", PlayerPrefs.GetInt("totalDiamonds") + PlayerPrefs.GetInt("diamonds"));
    }

    private IEnumerator DiamondAnimation(int remain, int totalDiamonds)
    {

        yield return new WaitForSeconds(0.1f);
        if (remain > 0)
        {
            _totalDiamondsText.text = (totalDiamonds + 1).ToString();
            StartCoroutine(DiamondAnimation(remain - 1, totalDiamonds + 1));
            _diamondUI.transform.parent.gameObject.GetComponent<AudioSource>().Play();
            _diamondUI.GetComponent<Animator>().SetTrigger("Go");
        }
        else
        {
            PlayerPrefs.SetInt("diamonds", 0);
            _diamondUI.SetActive(false);
            //_diamondUI.GetComponent<Animator>().SetTrigger("Go");
            _tapToContinue.SetActive(true);
            StartCoroutine(TapToContinue());
        }
    }

    private IEnumerator TapToContinue()
    {
        yield return new WaitForSeconds(0.25f);
        _canGoNextLevel = true;
    }
    public void NextLevel()
    {
        if (_canGoNextLevel)
        {
            CameraController cameraController = FindObjectOfType<CameraController>();
            _levels.ActivateCurrentLevel();
            _gameManager.SetTurret();
            _gameManager.SetMirrors();
            cameraController.SetDesiredPosition();
            _confetti.SetActive(false);
            gameObject.SetActive(false);
        }


    }

}
