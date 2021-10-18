using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaPeixe : MonoBehaviour
{
    public Animator Boca;
    public Animator Olhos;
    private bool anima;
   // public Animator Barbatana;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!anima)
        {
            StartCoroutine("Anima");
            anima = true;
        }

       
       
    }

    IEnumerator Anima()
    {

            Boca.SetTrigger("Bateu");
            Olhos.SetTrigger("Bateu");
            // Barbatana.SetTrigger("Bateu");
            yield return new WaitForSecondsRealtime(0.2f);
        anima = false;


    }
}
