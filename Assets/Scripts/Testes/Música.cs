using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Música : MonoBehaviour
{
    private GameObject Trilha;

    void Start()
    {
        Trilha = GameObject.FindWithTag("Música");
        if (Trilha.GetComponent<AudioSource>().isPlaying == false)
            Trilha.GetComponent<AudioSource>().Play();

    }

    void Update()
    {

    }
}
