using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineImpulseCustom : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    private void Awake()
    {
        ForceFeedBackController.OnDamage += VerticalShake;
        ForceFeedBackController.OnExplosion += HorizontalShake;
        
    }

    private void OnDestroy()
    {
        ForceFeedBackController.OnDamage -= VerticalShake;
        ForceFeedBackController.OnExplosion  -= HorizontalShake;
        
    }

    private void HorizontalShake()
    {
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Explosion;
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform;
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = .3f;
        cinemachineImpulseSource.GenerateImpulse(new Vector3(0.2f, 0, 0));
    }

    private void VerticalShake()
    {
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Rumble;
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform;
        cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = .2f;
        cinemachineImpulseSource.GenerateImpulse(new Vector3(0f, 0.1f, 0));
    }

    [Serializable]
    struct ShakeProfile
    {
       
    }
}
