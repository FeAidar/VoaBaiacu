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
       MainRenderer.enabled = true;
       rb2D.constraints = RigidbodyConstraints2D.None;
        animator.speed = 1 / ActiveDuration;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent(out WaterManager waterEdge))
            Hit(movableSoundEmitter.WaterAudioClips);
        else
        {
            Hit(movableSoundEmitter.HitAudioClips);
        }
     
    }
    private void Hit(AudioClip[] clip)
    {
        movableSoundEmitter.PlayAudio(clip);
        
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
       CallScoreByTimeout(hazardType);
       PlayFinishParticle();
       ForceFeedBackController.ShakeHeavy();
    }

    public void PlayFinishParticle()
    {
        if (endingParticle)
        {MainRenderer.enabled = false;
            movableSoundEmitter.PlayAudio(movableSoundEmitter.EndAudioClips);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            ChangeCollidersState(false);
            endingParticle.OnStopped += ReturnToPool;
            endingParticle.ParticleSystem.Play();
        }
    }

    public override void CauseDamage()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        ReturnToPool();
    }
}
