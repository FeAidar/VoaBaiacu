using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballcollider : MonoBehaviour
{

    private bool _cancel;
    [SerializeField] private Line line;
    // Start is called before the first frame update
    
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
        if (_cancel) return;
        _cancel = true;
        line.drawManager.ChangeLinesAmount(-1);
        Destroy(transform.parent.gameObject, 10f * Time.deltaTime);
    }
}
