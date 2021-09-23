using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballcollider : MonoBehaviour
{
    private bool cancela;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bola") )
        {
            Cancela();

            
           
        }
    }

    void Cancela()
    {
        if (!cancela)
        {
            cancela = true;
            Destroy(transform.parent.gameObject, 10f * Time.deltaTime);
        }
    }
}
