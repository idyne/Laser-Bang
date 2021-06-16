using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackButton : MonoBehaviour
{
    [SerializeField] private GameObject _level;
    [SerializeField] private GameObject _menu;
    public void Back()
    {
        _level.GetComponent<Animator>().SetTrigger("Disappear");
        _menu.SetActive(true);
        FindObjectOfType<Controller>()._isDragged = false;
    }

}
