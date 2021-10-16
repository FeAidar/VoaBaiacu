using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMar : MonoBehaviour
{
    public float tempo;
    private float _timeRemaining;
    public Animator Mar;
    private Failed _failed;
    public Spawnerdeboia SpawnderdeBomba;
    private bool Foi;


    void OnEnable()
    {
        _timeRemaining = tempo;
        _failed = FindObjectOfType<Failed>();
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
                _failed.BombaCaiu = false;
                SpawnderdeBomba.enabled = true;
                Mar.SetTrigger("Volta");
                Foi = true;
            }
        }

    }


}