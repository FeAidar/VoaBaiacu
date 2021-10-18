using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animabomba : MonoBehaviour
{

    private Animator bomba;
    private float Tempo;

    private void Start()
    {
        bomba = this.GetComponentInChildren<Animator>();
        Tempo = this.GetComponent<Timer>().tempo;
        bomba.speed = 1 / Tempo;
    }
}
