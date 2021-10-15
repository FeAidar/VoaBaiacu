using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Failed : MonoBehaviour
{
    public bool BombaCaiu;
    public Animator Agua;

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            if(BombaCaiu)
            SceneManager.LoadScene("SampleScene");
            else
           
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 13f, ForceMode2D.Impulse);

        }

        if (collision.gameObject.CompareTag("Bomba"))
        {
            if (!BombaCaiu)
            {
                Destroy(collision.gameObject);
                BombaCaiu = true;
                Bombacaiu();

            }
            else
            SceneManager.LoadScene("SampleScene");



        }

    }

    void Bombacaiu()
    {
        Agua.SetTrigger("poluir");
        
 }
}
