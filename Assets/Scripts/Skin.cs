using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _check;
    public Image _image;
    private Button _button;
    private Skins _skins;
    private Shop _shop;
    public int _index;
    public bool _isBought = false;
    public int _price = 0;
    public GameObject _layer;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _skins = FindObjectOfType<Skins>();
        _button.onClick.AddListener(() => _skins.Select(_index));
        _isBought = PlayerPrefs.GetInt("skinBought_" + _index) == 1;
        if (_isBought)
            _layer.SetActive(false);
        _shop = FindObjectOfType<Shop>();
    }

    public void Choose()
    {
        _cursor.SetActive(true);
        if (_isBought)
            _check.SetActive(true);
    }

    public void Unchoose()
    {
        _cursor.SetActive(false);
        _check.SetActive(false);
    }

    public void Buy()
    {
        if(PlayerPrefs.GetInt("totalDiamonds") >= _price)
        {
            PlayerPrefs.SetInt("skinBought_" + _index, 1);
            PlayerPrefs.SetInt("totalDiamonds", PlayerPrefs.GetInt("totalDiamonds") - _price);
            _shop._totalDiamonds.text = PlayerPrefs.GetInt("totalDiamonds").ToString();
            _isBought = true;
            _layer.SetActive(false);
            _check.SetActive(true);
        }
    }
}
