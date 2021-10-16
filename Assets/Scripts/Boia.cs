using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boia : MonoBehaviour
{
    private bool cancela;
    private ScoreGiver _scoreGiver;

    private void Start()
    {
        _scoreGiver = FindObjectOfType<ScoreGiver>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            VitoriaArco();
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce (transform.forward*0.5f, ForceMode2D.Impulse);



        }
    }

    public void TiraArco()
    {
        if (!cancela)
        {
            cancela = true;
            
            Destroy(gameObject, 5f * Time.deltaTime);
        }
    }

    void VitoriaArco()
    {
        if (!cancela)
        {
            Handheld.Vibrate();
            cancela = true;
            this.GetComponent<Collider2D>().enabled = false;
            Debug.Log("teste");
            _scoreGiver.StartCoroutine("GivePointRing");
            Destroy(gameObject, 5f * Time.deltaTime);
        }


    }

}
