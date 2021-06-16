using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    [SerializeField]
    private GameObject _completePanel;
    [SerializeField]
    private Animator _levelAnimator;
    //private float res = 0;
    private Controller _controller;
    [SerializeField]
    private GameObject _confetti;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ScreenCapture.CaptureScreenshot("d:\\laserbang.png");
        }
    }
    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _controller = FindObjectOfType<Controller>();
        Time.timeScale = 1;
    }

    private void Start()
    {
        SetTurret();
        SetMirrors();
        _controller.ChangeTurretColor();
    }


    public void CompleteLevel(float delay)
    {
        _completePanel.GetComponent<CompletePanel>()._text.text = "LEVEL COMPLETED\n" + _levelManager._currentLevel;
        _levelManager.CompleteLevel();
        _levelAnimator.SetTrigger("Disappear");
        StartCoroutine(_CompleteLevel(delay));
    }

    private IEnumerator _CompleteLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _completePanel.SetActive(true);
        _completePanel.GetComponent<CompletePanel>().Animation();
        _confetti.SetActive(true);
        
    }

    public void SetTurret()
    {
        _controller.SetTurret(GameObject.Find("Level " + _levelManager._currentLevel).transform.Find("Turret").Find("Turret Bottom").Find("Turret Head").gameObject);
    }

    public void SetMirrors()
    {
        _controller.SetMirrors(GameObject.Find("Level " + _levelManager._currentLevel).transform.Find("Mirrors").GetComponent<Mirrors>());
    }

    public void IncrementDiamonds()
    {
        PlayerPrefs.SetInt("diamonds", PlayerPrefs.GetInt("diamonds") + 1);
    }

    public void ResetDiamonds()
    {
        PlayerPrefs.SetInt("diamonds", 0);
    }



    /*private void Test()
    {

        List<Vector3> posList = new List<Vector3>();
        List<Vector3> dirList = new List<Vector3>();
        List<float> lenList = new List<float>();


        for (float i = 30; i < 150; i += 1f)
        {
            bool test = true;
            Vector3 pos = transform.position;
            //Vector3 dir = transform.TransformDirection(Vector3.forward);
            Vector3 dir = Quaternion.AngleAxis(i, transform.TransformDirection(Vector3.down)) * Vector3.right;



            while (test)
            {
                RaycastHit hit;
                Physics.Raycast(pos, dir, out hit, Mathf.Infinity);

                if (hit.collider.tag == "Target")
                {
                    posList.Add(pos);
                    dirList.Add(dir);
                    lenList.Add(hit.distance);
                    DrawLine(posList, dirList, lenList);
                    posList.Clear();
                    dirList.Clear();
                    lenList.Clear();
                    //Debug.DrawRay(pos, dir * hit.distance, Color.blue, 30);
                    res++;
                    test = false;
                }
                else if (hit.collider.tag == "Mirror")
                {
                    posList.Add(pos);
                    dirList.Add(dir);
                    lenList.Add(hit.distance);
                    //Debug.DrawRay(pos, dir * hit.distance, Color.green, 30);
                    //Debug.Log("devamke");
                    // set new pos
                    pos = hit.point;
                    //set new direction
                    dir = Vector3.Reflect(dir, hit.normal);
                }
                else
                {
                    posList.Clear();
                    dirList.Clear();
                    lenList.Clear();
                    //Debug.DrawRay(pos, dir * 100, Color.black, 30);
                    //Debug.Log("Lose");
                    test = false;
                }
            }
        }
        print("Success: " + res);
        print("Success ratio: %" + (res / 850) * 100);
    }

    private void DrawLine(List<Vector3> pos, List<Vector3> dir, List<float> len)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            Debug.DrawRay(pos[i], dir[i] * len[i], Color.green, 100);
        }
    }*/


}
