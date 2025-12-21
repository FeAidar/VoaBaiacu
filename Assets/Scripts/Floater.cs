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

    [SerializeField] private WinHitPoint winHitPoint;
    [SerializeField] private Animator animator;
    [SerializeField] ParticleNotifier endingParticle;
    [SerializeField] private ParticleSystem startingParticle;
    protected override void OnEnable()
    {
        base.OnEnable();
        startingParticle.Play();
        animator.SetTrigger(ShowUp);
    }
protected internal override void Victory()
 {
     ChangeCollidersState(false);
     if (endingParticle)
     {
         endingParticle.OnStopped += ReturnToPool;
         endingParticle.ParticleSystem.Play();
     }
     

 }



 private void OnCollisionEnter2D(Collision2D other)
 {
     Hit();
     if (other.gameObject.TryGetComponent(out PlayerSettings playerSettings))
     {
 
         other.gameObject.GetComponent<Rigidbody2D>().AddForce (transform.forward*0.5f, ForceMode2D.Impulse);
     }
     
 }

 private void Hit()
 {
     animator.SetTrigger(Hit1);
 }

 protected override void Timeout()
 {
    ChangeCollidersState(false);
    animator.SetTrigger(Hide);
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








