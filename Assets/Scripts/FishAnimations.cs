using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FishAnimations : MonoBehaviour
{
    private static readonly int Bateu = Animator.StringToHash("Bateu");
    [SerializeField] private Animator[] fangs;
    [SerializeField] private Animator mouth;
    [SerializeField] private Animator eyes;
    [FormerlySerializedAs("soundEmitter")] [SerializeField] private MovableSoundEmitter movableSoundEmitter;
    private bool _runningAnimation;
    private bool _canAnimate;

    public void Animate(bool canAnimate)
    {
            foreach (Animator animator in fangs)
            {
                animator.enabled =  canAnimate;
            }
            _canAnimate = canAnimate;
        
    }


   private void OnCollisionEnter2D(Collision2D collision)
   {
        if (!_canAnimate) return;
        if (_runningAnimation) return;
        if (collision.gameObject.TryGetComponent(out WinHitPoint winHitPoint)) return;
        movableSoundEmitter.PlayAudio(collision.gameObject.TryGetComponent(out WaterManager waterEdge)
            ? movableSoundEmitter.WaterAudioClips
            : movableSoundEmitter.HitAudioClips);
        StartCoroutine(Hit());
        _runningAnimation = true;
        
        
    }

    private IEnumerator Hit()
    {

            mouth.SetTrigger(Bateu);
            eyes.SetTrigger(Bateu);
           yield return new WaitForSecondsRealtime(0.1f);
            _runningAnimation = false;


    }
}
