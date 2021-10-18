using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGiver : MonoBehaviour
{
    public int PontosLinha;
    public int PontosArco;
    public int PontosAgua;
    public int PontosBomba;
    public Text Score;
    public int Pontos;
    private bool Desliga;
    public Text vidas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pontos < 0)
            Pontos = 0;
        Score.text = "" + Pontos;
        vidas.text = "Vidas: " + FindObjectOfType<PowerUps>().Vida;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Linha"))
        {
           // Debug.Log("teste");
            StartCoroutine ("GivePointLinha");
        }

  }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Mar"))
        {
            StartCoroutine("GivePointMar");
        }
    }

    IEnumerator GivePointLinha()
    {
        
        if (!Desliga)
        {
            Desliga = true;
            Pontos = Pontos+PontosLinha;
        }
        yield return new WaitForSecondsRealtime (0.2f);
        Desliga = false;

    }

    IEnumerator GivePointRing()
    {

        if (!Desliga)
        {
            Desliga = true;
            Pontos = Pontos + PontosArco;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator GivePointMar()
    {

        if (!Desliga)
        {
            Desliga = true;
            Pontos = Pontos + PontosAgua;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator GivePointBomb()
    {

        if (!Desliga)
        {
            Desliga = true;
            Pontos = Pontos + PontosBomba;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }



}
