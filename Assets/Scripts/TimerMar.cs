using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMar : MonoBehaviour
{
    public float tempo;
    private float _timeRemaining;
    private WaterManager _waterManager;
    private bool Foi;


    void OnEnable()
    {
        _timeRemaining = tempo;
        _waterManager = FindObjectOfType<WaterManager>();
        Foi = false;
    }

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

        }
        else
        {
            if (!Foi)
            {
                _timeRemaining = 0;
                //_waterManager.VoltaMar();
              // _waterManager.MarCausaDano = false;
                Foi = true;
            }
        }

    }


}