using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroyer : MonoBehaviour
{
    public float tempo;
    private float _timeRemaining;

    private void Start()
    {
        _timeRemaining = tempo;
    }

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

        }
        else
        {
            _timeRemaining = 0;
            Destroy(gameObject);
        }

    }

}