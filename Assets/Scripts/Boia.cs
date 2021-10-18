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
            yield return new WaitForSecondsRealtime(0.4f);
            Destroy(gameObject);
        }
    }



    public IEnumerator VitoriaArco()
    {

        if (!cancela)
        {
            
            cancela = true;
            _animator.SetTrigger("some");
            particula.SetActive(true);
            this.GetComponent<Collider2D>().enabled = false;
            //Debug.Log("teste");
            _scoreGiver.StartCoroutine("GivePointRing");
            yield return new WaitForSecondsRealtime(0.4f);
            Destroy(gameObject);

        }


    }

}
