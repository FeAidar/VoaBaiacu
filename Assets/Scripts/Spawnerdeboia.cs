using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerdeboia : MonoBehaviour
{ 
    public float min_X = -2f, max_X = 2f;
    public float min_Y = -14f, max_Y = 14f;
    public GameObject[] Objeto;
    float screenRatio;
    float widthOrtho;

    public float Timer = 2f;
    public int VariacaoDeTempo;
    

    // Start is called before the first frame update
    void OnEnable()
    {

            Invoke("Spawn", Timer + Random.Range(Timer * (VariacaoDeTempo * -0.01f), Timer * (VariacaoDeTempo * 0.01f)));

    }

    void Spawn()
    {
        float pos_X = Random.Range(min_X, max_X);
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        temp.y = pos_Y;

        Instantiate(Objeto[Random.Range(0, Objeto.Length)], temp, Quaternion.identity);


            Invoke("Spawn", Timer + Random.Range(Timer * (VariacaoDeTempo * -0.01f), Timer * (VariacaoDeTempo * 0.01f)));


    }

    void OnDisable()
    {
        CancelInvoke("Spawn");
    }

}
