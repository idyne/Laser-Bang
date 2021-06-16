using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private Shop _shop;
    public Text _text;
    private Skins _skins;

    private void Awake()
    {
        _shop = FindObjectOfType<Shop>();
        _skins = FindObjectOfType<Skins>();
    }
    public void Buy()
    {
        if (_shop._selectedSkin)
        {
            _shop._selectedSkin.Buy();
            _skins.Select(_shop._selectedSkin._index);
        }
            
    }
}
