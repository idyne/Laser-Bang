using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _autoAngle = 90;
    [SerializeField] private GameObject _sparks;
    [SerializeField] private GameObject _previewSparksPrefab;
    [SerializeField] private GameObject _line;
    [SerializeField] private GameObject _turret;
    [SerializeField] private Material _laserPreviewMaterial;
    [SerializeField] private float _laserDelay = 0.2f;
    [SerializeField] private GameObject _failSpark;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _level;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private GameObject _pow;
    [SerializeField] private GameObject _decal;
    private GameObject previewLine1;
    private GameObject previewLine2;
    private MeshRenderer previewMeshRenderer;
    private bool _autoRotate = false;

    private Mirrors _mirrors;

    public GameObject _menu;

    public int _reflections;
    public float _maxLength;

    private Ray _ray;
    private RaycastHit _hit;
    public bool _isLaserSend = false;
    private List<Collectible> _collectibles;
    private Touch _touch;
    public bool _isDragged = false;

    private List<Vector3> _positions;
    private bool _isPreviewSparkSet = false;
    [HideInInspector] public GameObject _previewSparks;


    private GameManager _gameManager;

    private CameraShake _cameraShake;

    private AudioSource _audioSource;
    private int _score = 0;

    private void Awake()
    {
        _positions = new List<Vector3>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();


        _audioSource = GetComponent<AudioSource>();
        _collectibles = new List<Collectible>();
        PlayerPrefs.SetInt("score", 0);

    }

    private float DegreeToRadian(float x)
    {
        return x / 180 * Mathf.PI;
    }


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if (!_isLaserSend)
            LaserPreview();
        if (Input.GetKeyDown(KeyCode.K))
            _autoRotate = !_autoRotate;
        if (_autoRotate)
        {
            if(Mathf.Abs(_turret.transform.rotation.eulerAngles.y - _autoAngle) > 2.1f)
            _turret.transform.Rotate(Vector3.up, 1);
            else
            {
                SendLaser();
                _autoRotate = false;
            }
        }
        
    }


    public void SendLaser()
    {
        if (!_isLaserSend)
        {
            Destroy(previewLine1);
            Destroy(previewLine2);
            //_settingsButton.GetComponent<Button>().interactable = false;
            _previewSparks.SetActive(false);
            GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
            for (int i = 0; i < lines.Length; ++i)
            {
                Destroy(lines[i]);
            }
            _positions.Clear();


            Vector3 pos = new Vector3(_turret.transform.position.x, 0.37f, _turret.transform.position.z);
            _positions.Add(pos);
            Vector3 dir = _turret.transform.forward;
            _ray = new Ray(pos, dir);
            StartCoroutine(SinglePath(_reflections, _laserDelay));
            _isLaserSend = true;
        }
    }

    private IEnumerator SinglePath(int reflection, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (reflection > 0)
        {
            int layerMask = ~LayerMask.GetMask("Collectible");
            Physics.Raycast(_ray.origin, _ray.direction, out _hit, Mathf.Infinity, layerMask);
            _score += Mathf.RoundToInt(Mathf.Abs(Vector3.Distance(_ray.origin, _hit.point) * 50)) + 100;
            _positions.Add(_hit.point);
            while (_hit.collider.tag == "Portal")
            {
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));
            }
            GameObject line = Instantiate(_line, (_positions[_positions.Count - 1] - _positions[_positions.Count - 2]) / 2 + _positions[_positions.Count - 2], Quaternion.Euler(
                90,
                (-180 / Mathf.PI) * Mathf.Atan(((_positions[_positions.Count - 1]).z - (_positions[_positions.Count - 2]).z) / ((_positions[_positions.Count - 1]).x - (_positions[_positions.Count - 2]).x)),
                90));

            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                Vector3.Distance((_positions[_positions.Count - 1]), _positions[_positions.Count - 2]) / 2,
                line.transform.localScale.z);
            Transform effect = line.transform.GetChild(0);
            ParticleSystem.MainModule main1 = effect.GetComponent<ParticleSystem>().main;
            main1.startLifetime = line.transform.localScale.y / (main1.startSpeed.constant / 2);

            Collider[] hitColliders = Physics.OverlapBox(line.transform.position, line.transform.localScale, line.transform.rotation);
            Collectible collectible;
            for (int i = 0; i < hitColliders.Length; ++i)
            {
                collectible = hitColliders[i].GetComponent<Collectible>();
                if (collectible)
                {
                    _collectibles.Add(collectible);
                    collectible.Collect();
                    _score += 500;
                }

            }
            ParticleSystem.ShapeModule shape = Instantiate(_sparks, _hit.point, Quaternion.identity).GetComponent<ParticleSystem>().shape;
            shape.rotation = new Vector3(_hit.normal.x * 90, 90, _hit.normal.z * 90);

            if (_hit.collider.tag != "Mirror" && _hit.collider.tag != "Target")
            {
                /*ParticleSystem.ShapeModule failShape = Instantiate(_failSpark, _hit.point, Quaternion.identity).GetComponent<ParticleSystem>().shape;
                failShape.rotation = new Vector3(_hit.normal.x * 90, 90, _hit.normal.z * -90);*/
                _audioSource.clip = _failSound;
                if (PlayerPrefs.GetInt("sound") == 1)
                    _audioSource.Play();
                Reload(0.5f);
            }
            else if (_hit.collider.tag == "Target")
            {
                print(_turret.transform.rotation.y);
                _score += Mathf.RoundToInt((0.5f - Mathf.Abs(_hit.collider.transform.position.x - _hit.point.x)) / 0.5f * 1000);
                _audioSource.clip = _successSound;
                if (PlayerPrefs.GetInt("sound") == 1)
                    //_audioSource.Play();
                    Destroy(_hit.collider.gameObject);
                Instantiate(_explosionEffect, _hit.collider.transform.position, Quaternion.identity).transform.parent = _mirrors.transform.parent;
                Instantiate(_pow, _hit.collider.transform.position + (Vector3.up * 2) + (Vector3.back * 2), Quaternion.identity).transform.parent = _mirrors.transform.parent;
                Instantiate(_decal, new Vector3(_hit.collider.transform.position.x, 0.11f, _hit.collider.transform.position.z), _decal.transform.rotation).transform.parent = _mirrors.transform.parent;
                StartCoroutine(_cameraShake.Shake(0.2f, 0.2f));
                PlayerPrefs.SetInt("score", _score);
                _mirrors.DropMirrors();
                _collectibles.Clear();
                _gameManager.CompleteLevel(1f);
            }
            else
            {
                if (PlayerPrefs.GetInt("sound") == 1)
                    //line.GetComponent<AudioSource>().Play();
                    StartCoroutine(_cameraShake.Shake(0.01f, 0.01f));
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));

                reflection -= 1;
                StartCoroutine(SinglePath(reflection, delay));
            }

        }
        else
        {
            Reload(0.5f);
        }
    }

    private void LaserPreview()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        for (int i = 0; i < lines.Length; ++i)
        {
            Destroy(lines[i]);
        }
        _positions.Clear();

        Vector3 pos = new Vector3(_turret.transform.position.x, 0.37f, _turret.transform.position.z);
        _positions.Add(pos);
        Vector3 dir = _turret.transform.forward;
        Debug.DrawRay(pos, dir, Color.red);
        _ray = new Ray(pos, dir);
        float remainingLength = _maxLength;
        int layerMask = ~LayerMask.GetMask("Collectible");
        if (Physics.Raycast(_ray, out _hit, remainingLength, layerMask))
        {
            if (!_isPreviewSparkSet)
            {
                _previewSparks = Instantiate(_previewSparksPrefab, _hit.point, Quaternion.identity);
                _isPreviewSparkSet = true;
            }
            _previewSparks.transform.position = _hit.point;
            ParticleSystem.ShapeModule shape = _previewSparks.GetComponent<ParticleSystem>().shape;
            shape.rotation = new Vector3(_hit.normal.x * 90, 90, _hit.normal.z * 90);
            _positions.Add(_hit.point);
            GameObject line = null;
            if (!previewLine1)
            {
                line = Instantiate(_line, (_positions[_positions.Count - 1] - _positions[_positions.Count - 2]) / 2 + _positions[_positions.Count - 2], Quaternion.Euler(
                    90,
                    (-180 / Mathf.PI) * Mathf.Atan((_positions[_positions.Count - 1].z - (_positions[_positions.Count - 2]).z) / ((_positions[_positions.Count - 1]).x - (_positions[_positions.Count - 2]).x)),
                    90));
                previewLine1 = line;
                previewLine1.tag = "Untagged";
            }
            else
            {
                line = previewLine1;
                line.transform.position = (_positions[_positions.Count - 1] - _positions[_positions.Count - 2]) / 2 + _positions[_positions.Count - 2];
                line.transform.rotation = Quaternion.Euler(
                    90,
                    (-180 / Mathf.PI) * Mathf.Atan((_positions[_positions.Count - 1].z - (_positions[_positions.Count - 2]).z) / ((_positions[_positions.Count - 1]).x - (_positions[_positions.Count - 2]).x)),
                    90);
            }
            Transform effect = line.transform.GetChild(0);
            ParticleSystem.MainModule main1 = effect.GetComponent<ParticleSystem>().main;
            main1.startLifetime = line.transform.localScale.y / (main1.startSpeed.constant / 2);
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                Vector3.Distance(_positions[_positions.Count - 1], _positions[_positions.Count - 2]) / 2,
                line.transform.localScale.z);
            remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
            _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));
        }
        if (_hit.collider.tag == "Mirror" && Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength, layerMask))
        {
            if (_ray.direction.magnitude > (_hit.point - _positions[_positions.Count - 1]).magnitude)
                _positions.Add(_hit.point);
            else
                _positions.Add(_positions[_positions.Count - 1] + _ray.direction);
            GameObject linePreview = null;
            if (!previewLine2)
            {
                linePreview = Instantiate(_line, (_positions[_positions.Count - 1] - _positions[_positions.Count - 2]) / 2 + _positions[_positions.Count - 2], Quaternion.Euler(90, (-180 / Mathf.PI) * Mathf.Atan(((_positions[_positions.Count - 1]).z - (_positions[_positions.Count - 2]).z) / ((_positions[_positions.Count - 1]).x - (_positions[_positions.Count - 2]).x)), 90));
                previewLine2 = linePreview;
                previewLine2.tag = "Untagged";
                previewMeshRenderer = linePreview.GetComponent<MeshRenderer>();
            }
            else
            {
                linePreview = previewLine2;
                linePreview.transform.position = (_positions[_positions.Count - 1] - _positions[_positions.Count - 2]) / 2 + _positions[_positions.Count - 2];
                linePreview.transform.rotation = Quaternion.Euler(90, (-180 / Mathf.PI) * Mathf.Atan(((_positions[_positions.Count - 1]).z - (_positions[_positions.Count - 2]).z) / ((_positions[_positions.Count - 1]).x - (_positions[_positions.Count - 2]).x)), 90);
            }
            ParticleSystem.MainModule main2 = linePreview.transform.GetChild(0).GetComponent<ParticleSystem>().main;
            main2.startLifetime = linePreview.transform.localScale.y / (main2.startSpeed.constant / 2);
            linePreview.transform.localScale = new Vector3(
                linePreview.transform.localScale.x,
                Vector3.Distance(_positions[_positions.Count - 1], _positions[_positions.Count - 2]) / 2,
                linePreview.transform.localScale.z);
            Material[] materials = { _laserPreviewMaterial };
            linePreview.GetComponent<Renderer>().materials = materials;

        }
        else
        {
            Destroy(previewLine2);
        }


    }


    public void Reload(float delay)
    {

        StartCoroutine(_Reload(delay));
    }

    private IEnumerator _Reload(float delay)
    {
        yield return new WaitForSeconds(delay);
        _score = 0;
        _isLaserSend = false;
        _isPreviewSparkSet = false;
        _gameManager.ResetDiamonds();
        for (int i = 0; i < _collectibles.Count; ++i)
        {
            _collectibles[i].Activate();
        }
        _collectibles.Clear();
    }

    private void CheckInput()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                _text.text = transform.rotation.eulerAngles.y + "\n" + _touch.deltaPosition;
                transform.rotation = Quaternion.Euler(0, _touch.deltaPosition.x, 0);
                float rotX = Input.GetAxis("Mouse X") * 20 * Mathf.Deg2Rad;
                float rotY = Input.GetAxis("Mouse Y") * 20 * Mathf.Deg2Rad;

                transform.Rotate(Vector3.up, -rotX);
            }
        }
    }

    public void OnDrag()
    {
        if (FindObjectOfType<CameraController>()._isSet)
        {
            if (!_isDragged)
            {
                _menu.SetActive(false);
                _level.GetComponent<Animator>().SetTrigger("Appear");
                _isDragged = true;
            }
            float rotX = Input.GetAxis("Mouse X") * 100 * Mathf.Deg2Rad;
            _turret.transform.Rotate(Vector3.up, rotX);
        }

    }

    public void OnPointerUp()
    {
        if (_isDragged)
            SendLaser();
    }

    public void SetTurret(GameObject turret)
    {
        _turret = turret;
        _isDragged = false;
        _positions.Clear();
        ChangeTurretColor();
    }

    public void SetMirrors(Mirrors mirrors)
    {
        _mirrors = mirrors;
    }

    public void ChangeTurretColor()
    {
        _turret.transform.parent.GetComponent<TurretColor>().ChangeColor();
    }

    public void AutoRotate()
    {

    }
}
