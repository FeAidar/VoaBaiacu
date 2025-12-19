using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FishHitAnimations : MonoBehaviour
{
    [FormerlySerializedAs("Boca")] [SerializeField] private Animator mouth;
    [FormerlySerializedAs("Olhos")] [SerializeField] private Animator eyes;
    private bool _runningAnimation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_runningAnimation)
        {
            StartCoroutine("Hit");
            _runningAnimation = true;
        }

       
       
    }

    private IEnumerator Hit()
    {

            mouth.SetTrigger("Bateu");
            eyes.SetTrigger("Bateu");
           yield return new WaitForSecondsRealtime(0.2f);
            _runningAnimation = false;


    }
}
