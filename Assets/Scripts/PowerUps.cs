using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour
{
    public GameObject paraquedas;
    public GameObject mascara;
    private ScoreGiver Score;
    private int pontos;
    public int praganhar;
    private float gravidadenormal;
    private float pesonormal;
    public float GravidadeParaquedas;
    private float pontoinicial;
    private bool valendo;
    public int Vida = 3;
    private Failed _failed;
    private bool _mascara;
    public GameObject CanvasMascara;
    public GameObject CanvasParaquedas;
    private bool perdeparaqueda;
    private bool check;
    private Menu _menu;
    public AudioSource PowerUP;

    private void Start()
    {
        Score = GetComponent<ScoreGiver>();
        pontoinicial = Score.scoreValue;
        pesonormal = GetComponent<Rigidbody2D>().mass;
        gravidadenormal = GetComponent<Rigidbody2D>().gravityScale;
        _failed = FindObjectOfType<Failed>();
        _menu = FindObjectOfType<Menu>();

    }

    void Update()
    {
        pontos = Score.scoreValue;
        if (pontos > pontoinicial + praganhar)
        {
            if (!valendo)
            {
                int sorteio;
                sorteio = Random.Range(1, 3);

                if (sorteio == 1)
                {
                    Paraquedas();
                    valendo = true;
                }
                if (sorteio == 2)
                {
                    Mascara();
                    valendo = true;
                }
                
            }


        }

    }


    void Paraquedas()
    {
        PowerUP.Play();
        paraquedas.SetActive(true);
        CanvasParaquedas.SetActive(true);
        GetComponent<Rigidbody2D>().mass = 1;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().SetRotation(0);
        GetComponent<Rigidbody2D>().gravityScale = GravidadeParaquedas;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        perdeparaqueda = true;
    }

    void Mascara()
    {
        PowerUP.Play();
        mascara.SetActive(true);
        CanvasMascara.SetActive(true);
        _mascara = true;
        


    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mar"))
        {
            
            if (_failed.MarCausaDano == true)
            {

                if (_mascara == true)
                {
                    if (Vida > 0)
                    {
                        if (!check)
                        {
                            //    Debug.Log("bateu");
                            StartCoroutine("PerdeVida");
                            check = true;
                        }

                    }

                    if (Vida <= 0)
                    {
                        {
                            StartCoroutine("Termina");
                        }
                    }

                }
                else
                {
                   _menu.Perdeu();
                }

            }



        }

        if (collision.gameObject.CompareTag("Linha"))
        {

           // Debug.Log("colidiu");
            if (perdeparaqueda)
            {

                if (Vida > 0)
                {


                    StartCoroutine("PerdeVida");
                }

                if (Vida <= 0)
                {
                    this.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
                    StartCoroutine("Termina");
                }

            }
        }

    }



  
    IEnumerator PerdeVida()
    {

        Vida--;

        yield return new WaitForSecondsRealtime(0.2f);
       
        if (Vida == 2)
        {
            CanvasMascara.GetComponent<LifeGauge>().duasvidas();
            CanvasParaquedas.GetComponent<LifeGauge>().duasvidas();
        }

        if (Vida == 1)
        {
            CanvasMascara.GetComponent<LifeGauge>().umavida();
            CanvasParaquedas.GetComponent<LifeGauge>().umavida();
        }
        if(Vida == 0)
        {
            CanvasParaquedas.SetActive(false);
            CanvasMascara.SetActive(false);
        }

        check = false;

    }

    IEnumerator Termina()
    {
        perdeparaqueda = false;
        valendo = false;
        pontoinicial = pontos;
        paraquedas.SetActive(false);
        mascara.SetActive(false);
        CanvasMascara.SetActive(false);
        _mascara = false;
        
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Rigidbody2D>().gravityScale = gravidadenormal;
        GetComponent<Rigidbody2D>().mass = pesonormal;
        yield return new WaitForSecondsRealtime(0.2f);
        Vida = 3;

            CanvasMascara.GetComponent<LifeGauge>().tresvidas();
            CanvasParaquedas.GetComponent<LifeGauge>().tresvidas();
        

    }


    IEnumerator TerminaParaquedas()
    {
        perdeparaqueda = false;
        valendo = false;
        pontoinicial = pontos;
        paraquedas.SetActive(false);
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Rigidbody2D>().gravityScale = gravidadenormal;
        GetComponent<Rigidbody2D>().mass = pesonormal;
        yield return new WaitForSecondsRealtime(0.2f);
        Vida = 3;
        CanvasParaquedas.GetComponent<LifeGauge>().tresvidas();


    }

}
