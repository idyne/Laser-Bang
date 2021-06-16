using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Engel, Hedef, Duvar, Target, Turret;
    private float res = 0;
    public int _n = 2, _g = 0, _yol = 2;

    private void Start()
    {
        //Target.SetActive(false);
        //StartCoroutine(Generate(_n, _g, 5, 1.9f, 0.1f, 30, _yol));
        Test();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Test();
    }
    private IEnumerator Generate(int n, int g, float height, float width, float time, float minAngle, int yol)
    {
        yield return new WaitForSeconds(time);
        float minAngleFinal = minAngle * 2;

        List<List<Vector3>> posLists = new List<List<Vector3>>();
        List<List<Vector3>> dirLists = new List<List<Vector3>>();
        List<List<GameObject>> engelLists = new List<List<GameObject>>();
        List<List<GameObject>> duvarLists = new List<List<GameObject>>();

        for (int k = 0; k < yol; k++)
        {
            posLists.Add(new List<Vector3>());
            dirLists.Add(new List<Vector3>());
            engelLists.Add(new List<GameObject>());
            duvarLists.Add(new List<GameObject>());


            bool oldu = false;

            // rastgele duvar pozisyonları belirlenir
            posLists[k].Add(new Vector3(0, 0.341f, transform.position.z - 5));
            for (int i = 0; i < n; i++)
            {
                posLists[k].Add(new Vector3(Random.Range(-width, width), 0.341f, transform.position.z + Random.Range(-height, height)));
            }
            posLists[k].Add(new Vector3(0, 0.341f, transform.position.z + Target.transform.localPosition.z));
            for (int i = 0; i < g; i++)
            {
                posLists[k].Add(new Vector3(Random.Range(-width, width), 0.341f, transform.position.z + Random.Range(-height, height)));
            }

            // duvarlar oluşturulur
            for (int i = 0; i < n; i++)
            {
                engelLists[k].Add(Instantiate(Engel, posLists[k][i + 1], Engel.transform.rotation, transform.Find("Mirrors")));
            }
            for (int i = 0; i < g; i++)
            {
                engelLists[k].Add(Instantiate(Engel, posLists[k][i + n + 2], Engel.transform.rotation, transform.Find("Mirrors")));
            }

            // duvarların uyumu kontrol edilir
            for (int i = 0; i < n + 1; i++)
            {
                RaycastHit hit;
                Vector3 pos = posLists[k][i];
                Vector3 dir = posLists[k][i + 1] - posLists[k][i];
                dirLists[k].Add(dir);

                GameObject _hedef = Instantiate(Hedef, posLists[k][i + 1], Hedef.transform.rotation, transform.Find("Mirrors"));

                if (i != n)
                {
                    DestroyImmediate(engelLists[k][i]);
                }

                Physics.Raycast(pos, dir, out hit, Mathf.Infinity);
                Debug.DrawRay(pos, dir.normalized * hit.distance, Color.green, 0.01f);

                if (i != n)
                {
                    engelLists[k][i] = Instantiate(Engel, posLists[k][i + 1], Engel.transform.rotation, transform.Find("Mirrors"));
                }

                if (hit.collider.tag != "Hedef")
                {
                    DestroyImmediate(_hedef);
                    break;
                }
                else if (i == n)
                {
                    oldu = true;

                    for (int j = 0; j < dirLists[k].Count - 1; j++)
                    {

                        Vector2 dir1 = new Vector2(dirLists[k][j].x, dirLists[k][j].z);
                        Vector2 dir2 = new Vector2(dirLists[k][j + 1].x, dirLists[k][j + 1].z);

                        //print("açı: " + Vector2.Angle(dir1, dir2));
                        if (Vector2.Angle(dir1, dir2) < minAngleFinal)
                        {
                            oldu = false;
                            break;
                        }
                    }
                }

                DestroyImmediate(_hedef);

            }

            if (oldu)
            {
                for (int i = 0; i < engelLists[k].Count; i++)
                {
                    DestroyImmediate(engelLists[k][i]);
                }

                for (int i = 1; i < n + 1; i++)
                {

                    // 2 vektörü 2 boyutlu yap
                    Vector2 dir1 = new Vector2(dirLists[k][i - 1].x, dirLists[k][i - 1].z);

                    Vector2 dir2 = new Vector2(dirLists[k][i].x, dirLists[k][i].z);


                    // vektörlerin 2 boyutlu açısal farkını bul
                    float V2_angle = Vector2.SignedAngle(dir2, dir1);
                    //print("fark: " + V2_angle);
                    //print("yarı fark: " + V2_angle/2);

                    float ekleme = Vector2.SignedAngle(dir1, Vector2.right);
                    //print("ekleme: " + ekleme);

                    //print("toplam: " + (ekleme + V2_angle / 2));

                    // normal
                    //Debug.DrawRay(posList[i], (dirList[i - 1].normalized - dirList[i].normalized).normalized * 100, Color.blue, 1000);
                    Vector3 rotus = (dirLists[k][i - 1].normalized - dirLists[k][i].normalized).normalized / 20;
                    // açıyı kullan
                    duvarLists[k].Add(Instantiate(Duvar, posLists[k][i] + rotus, Quaternion.Euler(0f, ekleme + V2_angle / 2, 0f), transform.Find("Mirrors")));
                }

                for (int i = 2 + n; i < n + g + 2; i++)
                {
                    int ra = Random.Range(0, 180);
                    duvarLists[k].Add(Instantiate(Duvar, posLists[k][i], Quaternion.Euler(0f, ra, 0f), transform.Find("Mirrors")));
                }

                /*
                // yeni lazerle deneme
                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, transform.position);
                Vector3 pos = transform.position;
                Vector3 dir = posList[1] - pos;
                _ray = new Ray(pos, dir);

                StartCoroutine(SinglePath(_reflections));
                _isLaserSend = true;
                */
                if (yol - 1 == k)
                {
                    Target.SetActive(true);
                    Test();
                    print("Completed");
                }

            }
            else
            {

                for (int i = 0; i < engelLists[k].Count; i++)
                {
                    DestroyImmediate(engelLists[k][i]);
                }

                for (int part = 0; part < k; part++)
                {
                    for (int i = 0; i < duvarLists[part].Count; i++)
                    {
                        DestroyImmediate(duvarLists[part][i]);
                    }
                }

                StartCoroutine(Generate(n, g, height, width, time, minAngle, yol));
                break;
            }
        }





    }

    private void Test()
    {

        List<Vector3> posList = new List<Vector3>();
        List<Vector3> dirList = new List<Vector3>();
        List<float> lenList = new List<float>();

        int layerMask = ~LayerMask.GetMask("Collectible");
        for (float i = 0; i < 360; i += 0.2f)
        {
            bool test = true;
            Vector3 pos = Turret.transform.position;
            //Vector3 dir = transform.TransformDirection(Vector3.forward);
            Vector3 dir = Quaternion.AngleAxis(i, transform.TransformDirection(Vector3.down)) * Vector3.right;



            while (test)
            {
                RaycastHit hit;
                if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity, layerMask))
                {
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
                else
                {
                    Debug.DrawRay(pos, dir, Color.blue);
                    test = false;
                    print(gameObject.name);
                }


            }
        }
        /*print("Success: " + res);
        print("Success ratio: %" + (res / 850) * 100);*/
    }

    private void DrawLine(List<Vector3> pos, List<Vector3> dir, List<float> len)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            Debug.DrawRay(pos[i], dir[i] * len[i], Color.green, 10);
        }
    }


}
