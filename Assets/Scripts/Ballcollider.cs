using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballcollider : MonoBehaviour
{

    private bool cancela;
    private DrawManager _drawmanager;
    // Start is called before the first frame update

    private void Start()
    {
        _drawmanager = FindObjectOfType<DrawManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bola") )
        {
            Cancela();
          collision.gameObject.GetComponent<Rigidbody2D>().AddForce (transform.up*1.2f, ForceMode2D.Impulse);

            
           
        }

        if (collision.gameObject.CompareTag("Bomba"))
        {
            Cancela();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1.1f, ForceMode2D.Impulse);



        }
    }

    void Cancela()
    {
        if (!cancela)
        {
            cancela = true;

                _drawmanager.linha--;
           
            
            Destroy(transform.parent.gameObject, 10f * Time.deltaTime);
        }
    }
}
