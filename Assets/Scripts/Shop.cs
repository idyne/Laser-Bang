using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] BuyButton _buyButton;
    [SerializeField] BuyButton _disabledBuyButton;
    [SerializeField] Button _usingButton;
    [HideInInspector] public Skin _selectedSkin;
    public Text _totalDiamonds;
    private Skins _skins;

    private void Awake()
    {
        _skins = FindObjectOfType<Skins>();
    }

    public void ChangeButton(string state)
    {
        if (state == "buy")
        {
            if (_selectedSkin && PlayerPrefs.GetInt("totalDiamonds") >= _selectedSkin._price)
            {
                _buyButton._text.text = _selectedSkin._price.ToString();
                _buyButton.gameObject.SetActive(true);
                _disabledBuyButton.gameObject.SetActive(false);
            }
            else
            {
                _disabledBuyButton._text.text = _selectedSkin._price.ToString();
                _buyButton.gameObject.SetActive(false);
                _disabledBuyButton.gameObject.SetActive(true);
            }

            _usingButton.gameObject.SetActive(false);
        }
        else if (state == "using")
        {
            _buyButton.gameObject.SetActive(false);
            _usingButton.gameObject.SetActive(true);
            _disabledBuyButton.gameObject.SetActive(false);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _skins.Select(PlayerPrefs.GetInt("selectedSkin"));
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    


}
