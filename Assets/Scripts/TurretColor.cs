using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretColor : MonoBehaviour
{
    private Renderer _mesh;
    [SerializeField] private Material[] _materials;


    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        _mesh.material = _materials[PlayerPrefs.GetInt("selectedSkin")];
        transform.Find("Turret Head").GetComponent<MeshRenderer>().material = _materials[PlayerPrefs.GetInt("selectedSkin")];
    }
}
