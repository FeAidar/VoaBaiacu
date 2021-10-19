using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Failed : MonoBehaviour
{
    public bool BombaCaiu;
    public Animator Agua;
    private int Vida;
    public Spawnerdeboia SpawnderdeBomba;
    public bool mascara;
    private PowerUps _powerUps;
    public GameObject watersplash;
    public GameObject acidsplash;
    private float localdeimpacto;

    private void Start()
    {
        _powerUps = FindObjectOfType<PowerUps>();
        
    }

    private void Update()
    {
        Vida = _powerUps.Vida;
      //  Debug.Log(Vida);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            if (!BombaCaiu)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1.5f, ForceMode2D.Impulse);
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                if (Vida > 0)
                    localdeimpacto = collision.gameObject.transform.position.x;
                watersplash.transform.position = new Vector3(localdeimpacto, watersplash.transform.position.y);
                StartCoroutine("PerdeVida");

            }

            if (BombaCaiu)
            {
                if (Vida > 0)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1.5f, ForceMode2D.Impulse);

                    this.gameObject.GetComponent<Collider2D>().enabled = false;
                    localdeimpacto = collision.gameObject.transform.position.x;
                    acidsplash.transform.position = new Vector3(localdeimpacto, acidsplash.transform.position.y);

                    StartCoroutine("PerdeVida");
                }


            }



        }

        if (collision.gameObject.CompareTag("Bomba"))
        {
            if (!BombaCaiu)
            {
                Destroy(collision.gameObject);
                BombaCaiu = true;
                Handheld.Vibrate();
                Bombacaiu();

            }
            else return;

            localdeimpacto = collision.gameObject.transform.position.x;
            watersplash.transform.position = new Vector3(localdeimpacto, watersplash.transform.position.y);

        }

    }

    void Bombacaiu()
    {
        
        watersplash.GetComponent<ParticleSystem>().Play();
        Agua.SetTrigger("poluir");
        SpawnderdeBomba.enabled = false;
        
 }
    IEnumerator PerdeVida()
    {
       if(!BombaCaiu)
        watersplash.GetComponent<ParticleSystem>().Play();
       else
            acidsplash.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(0.2f);
        this.gameObject.GetComponent<Collider2D>().enabled = true;

    }
}
