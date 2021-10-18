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
    private float TempodeSpawn;
    private int VariacaoDeTempo;


    public float TempoDeSpawnManha;
    public float TempoDeSpawnTarde;
    public float TempoDeSpawnoite;
    public int VariacaoDeTempoManha;
    public int VariacaoDeTempoTarde;
    public int VariacaoDeTempoNoite;

    // Start is called before the first frame update
    void OnEnable()
    {

        Invoke("Spawn", TempodeSpawn + Random.Range(TempodeSpawn * (VariacaoDeTempo * -0.01f), TempodeSpawn * (VariacaoDeTempo * 0.01f)));

    }

    void Spawn()
    {
        float pos_X = Random.Range(min_X, max_X);
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        temp.y = pos_Y;

        Instantiate(Objeto[Random.Range(0, Objeto.Length)], temp, Quaternion.identity);


        Invoke("Spawn", TempodeSpawn + Random.Range(TempodeSpawn * (VariacaoDeTempo * -0.01f), TempodeSpawn * (VariacaoDeTempo * 0.01f)));


    }

    void OnDisable()
    {
        CancelInvoke("Spawn");
    }

    public void TemposManha()
    {
        TempodeSpawn = TempoDeSpawnManha;
        VariacaoDeTempo = VariacaoDeTempoManha;
        }

    public void TemposTarde()
    {
        TempodeSpawn = TempoDeSpawnTarde;
        VariacaoDeTempo = VariacaoDeTempoTarde;
    }
    public void TemposNoite()
    {
        TempodeSpawn = TempoDeSpawnoite;
        VariacaoDeTempo = VariacaoDeTempoNoite;
    }
}




