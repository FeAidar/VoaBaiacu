using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : SpawnableObject
{

    [SerializeField] private Animator animator;
    [SerializeField] ParticleNotifier endingParticle;

    
    protected override void OnEnable()
    {
       base.OnEnable();
       rb2D.constraints = RigidbodyConstraints2D.None;
        animator.speed = 1 / activeDuration;
    }
    
    protected  override void ReturnToPool()
    {
        if (endingParticle)
        {
            endingParticle.OnStopped -= ReturnToPool;
        }
        base.ReturnToPool();
    }


    protected override void Timeout()
    {
       PlayFinishParticle();
    }

    public void PlayFinishParticle()
    {
        if (endingParticle)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            ChangeCollidersState(false);
            endingParticle.OnStopped += ReturnToPool;
            endingParticle.ParticleSystem.Play();
        }
    }
}
