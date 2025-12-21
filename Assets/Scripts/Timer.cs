using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float tempo;
    private float _timeRemaining;
    public bool Linha;
    public bool Arco;
    public bool Bomba;
    private bool chamou;

    private void Start()
    {
        _timeRemaining = tempo;
    }

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            chamou = false;

        }
        else
        {
            _timeRemaining = 0;
            if (Linha)
            if(!chamou)
                //GetComponent<Line>().RemoveLineCount();

            if (Arco)
                if (!chamou)
                    GetComponent<Floater>().StartCoroutine("TiraArco");

            if (Bomba)
                if (!chamou)
                    GetComponent<Bomb>().StartCoroutine("TiraBomba");
            chamou = true;
        }

    }

}