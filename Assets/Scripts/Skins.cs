using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    [SerializeField] Skin[] _skins;
    [SerializeField] Animator _currentSkin;
    [SerializeField] Image _currentImage;
    [SerializeField] Image _currentImageD;
    [HideInInspector] public int _selectedIndex = 0;
    private Shop _shop;

    private void Awake()
    {
        _shop = FindObjectOfType<Shop>();
    }

    private void Start()
    {
        Select(PlayerPrefs.GetInt("selectedSkin"));
    }

    public void Select(int index)
    {
        _selectedIndex = index;
        for (int i = 0; i < _skins.Length; ++i)
        {
            if (i == index)
            {
                _skins[i].Choose();
                _shop._selectedSkin = _skins[i];
                if (_skins[i]._isBought)
                {
                    _shop.ChangeButton("using");
                    _currentImage.sprite = _shop._selectedSkin._image.sprite;
                    _currentSkin.SetTrigger("Change");
                    _currentImageD.sprite = _shop._selectedSkin._image.sprite;
                    PlayerPrefs.SetInt("selectedSkin", _shop._selectedSkin._index);
                    FindObjectOfType<Controller>().ChangeTurretColor();
                }
                else
                {
                    _shop.ChangeButton("buy");
                }
            }
            else
                _skins[i].Unchoose();
        }
    }
}
