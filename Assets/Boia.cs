using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boia : MonoBehaviour
{
    private bool cancela;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            TiraArco();
            // collision.gameObject.GetComponent<Rigidbody2D>().AddForce (transform.up*0.5f, ForceMode2D.Impulse);



        }
    }

    public void TiraArco()
    {
        if (!cancela)
        {
            cancela = true;
            Handheld.Vibrate();
            Destroy(gameObject, 5f * Time.deltaTime);
        }
    }

}
