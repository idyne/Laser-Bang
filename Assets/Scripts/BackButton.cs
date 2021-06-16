using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private ButtonController _buttonController;
    private void Awake()
    {
        _buttonController = FindObjectOfType<ButtonController>();
    }

    public void PlaySound()
    {   
        _buttonController.PlaySound();
    }
}
