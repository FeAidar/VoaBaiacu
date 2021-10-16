using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Failed : MonoBehaviour
{
    public bool BombaCaiu;
    public Animator Agua;
    public int Vida = 3;
    public Spawnerdeboia SpawnderdeBomba;


        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            if(BombaCaiu)
            SceneManager.LoadScene("SampleScene");
            else
           
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 13f, ForceMode2D.Impulse);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            if (Vida >0)
            StartCoroutine("PerdeVida");
            if (Vida <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }

        }

        if (collision.gameObject.CompareTag("Bomba"))
        {
            if (!BombaCaiu)
            {
                Destroy(collision.gameObject);
                BombaCaiu = true;
                Bombacaiu();

            }
            else return;
            



        }

    }

    void Bombacaiu()
    {
        Agua.SetTrigger("poluir");
        SpawnderdeBomba.enabled = false;
        
 }
    IEnumerator PerdeVida()
    {
        Vida--;
        yield return new WaitForSecondsRealtime(0.2f);
        this.gameObject.GetComponent<Collider2D>().enabled = true;

    }
}
