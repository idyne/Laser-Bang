using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diamond : MonoBehaviour
{
    public Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    private void Start()
    {
        SetDiamonds();
    }

    public void SetDiamonds()
    {
        _text.text = PlayerPrefs.GetInt("totalDiamonds").ToString();
    }
}
