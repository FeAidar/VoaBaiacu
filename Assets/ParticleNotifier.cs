using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleNotifier : MonoBehaviour
{
    public Action OnStopped;
    [SerializeField] private ParticleSystem _particleSystem;
    public ParticleSystem ParticleSystem => _particleSystem;
    private void OnParticleSystemStopped()
    {
        OnStopped?.Invoke();
    }
}