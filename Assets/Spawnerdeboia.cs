using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerdeboia : MonoBehaviour
{ 
    public float min_X = -2f, max_X = 2f;
    public float min_Y = -14f, max_Y = 14f;
    public GameObject[] boia;
    float screenRatio;
    float widthOrtho;

    public float timer = 2f;

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("SpawnBoia", timer);

    }

    void SpawnBoia()
    {
        float pos_X = Random.Range(min_X, max_X);
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        temp.y = pos_Y;

        Instantiate(boia[Random.Range(0, boia.Length)], temp, Quaternion.identity);

        Invoke("SpawnBoia", timer);


    }

    void OnDisable()
    {
        CancelInvoke("SpawnBoia");
    }

}
