using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boia : MonoBehaviour
{
    private bool cancela;
    private ScoreGiver _scoreGiver;
    private Animator _animator;
    public GameObject particula;

    private void Start()
    {
        _scoreGiver = FindObjectOfType<ScoreGiver>();
        _animator = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            StartCoroutine("VitoriaArco");
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce (transform.forward*0.5f, ForceMode2D.Impulse);



        }
    }

    public IEnumerator TiraArco()
    {
        if (!cancela)
        {
            cancela = true;
             _animator.SetTrigger("some");
            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("SomeBoia"))
            {
                yield return null;
            }
            while ((_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
            {
                yield return null;
            }
            Destroy(gameObject);
        }



    }



    public IEnumerator VitoriaArco()
    {

        if (!cancela)
        {
            
            cancela = true;
            particula.SetActive(true);
            _animator.SetTrigger("some");
            this.GetComponent<Collider2D>().enabled = false;
            _scoreGiver.StartCoroutine("GivePointRing");
            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("SomeBoia"))
            {
                yield return null;
            }
            while ((_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
            {
                yield return null;
            }
            
             Destroy(gameObject);

        }


    }

}
