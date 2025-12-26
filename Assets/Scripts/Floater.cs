using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Floater : SpawnableObject
{
    private static readonly int Hit1 = Animator.StringToHash("Hit");
    private static readonly int Hide = Animator.StringToHash("Hide");
    private static readonly int ShowUp = Animator.StringToHash("ShowUp");

    
    [SerializeField] private Animator animator;
    [SerializeField] ParticleNotifier endingParticle;
    [SerializeField] private ParticleSystem startingParticle;
    
   
    protected override void OnEnable()
    {
        base.OnEnable();
        ChangeCollidersState(false);
        startingParticle.Play();
        animator.SetTrigger(ShowUp);
        movableSoundEmitter.PlayAudio(movableSoundEmitter.AppearAudioClips);
        DOVirtual.DelayedCall(0.3f, () => ChangeCollidersState(true));
    }
protected internal override void Victory()
 {
     ChangeCollidersState(false);
     if (endingParticle)
     {
         endingParticle.OnStopped += ReturnToPool;
         endingParticle.ParticleSystem.Play();
         movableSoundEmitter.PlayAudio(movableSoundEmitter.EndAudioClips);
     }
     

 }



 private void OnCollisionEnter2D(Collision2D other)
 {
     Hit();
     if (other.gameObject.TryGetComponent(out PlayerSettings playerSettings))
     {
         ContactPoint2D contact = other.GetContact(0);
         Vector2 dir = -contact.normal;
         Vector2 finalDir = (dir + Vector2.up).normalized;
         playerSettings.Rigidbody2D.AddForce(finalDir * 0.25f, ForceMode2D.Impulse);
         ForceFeedBackController.ShakeLightTouch();
         
     }

     if (other.gameObject.TryGetComponent(out SpawnableObject spawnableObject))
     {
         ContactPoint2D contact = other.GetContact(0);
         Vector2 dir = -contact.normal;
         Vector2 finalDir = (dir + Vector2.up).normalized;
         spawnableObject.Rigidbody2D.AddForce(finalDir * 0.25f, ForceMode2D.Impulse);
     }
     
 }

 private void Hit()
 {
     animator.SetTrigger(Hit1);
     movableSoundEmitter.PlayAudio(movableSoundEmitter.HitAudioClips);
 }

 protected override void Timeout()
 {
    
    animator.SetTrigger(Hide);
    DOVirtual.DelayedCall(0.3f, () => ChangeCollidersState(false));
    DOVirtual.DelayedCall(0.5f, () => ReturnToPool());
 }
 
 protected override void  ReturnToPool()
 {
     if (endingParticle)
     {
         endingParticle.OnStopped -= ReturnToPool;
     }
     base.ReturnToPool();
 }
}








