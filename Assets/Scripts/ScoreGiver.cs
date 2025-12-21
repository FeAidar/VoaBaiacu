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
    public TMPro.TextMeshProUGUI score;
    private bool Desliga;
    public GameObject highscore;
    public int pontos;
    const string bestID = "Score_Best";
    int highScore;
    public int scoreValue = 0;




    void Start()
    {

        highScore = PlayerPrefs.GetInt(bestID, 0);
        
    }

    public void AddScore(int addScore)
    {
        scoreValue += addScore;
        if (scoreValue < 0)
            scoreValue = 0;
        score.text = scoreValue.ToString();


        if (scoreValue > highScore)
        {
            ShowHighScore();
        }

       if (scoreValue < highScore)
         {
              HideHighScore();
          }
    }

    public void RegisterScore()
    {
        if (scoreValue > highScore)
        {
            highScore = scoreValue;
            PlayerPrefs.SetInt(bestID, highScore);
            ShowHighScore();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Linha"))
        {
           // Debug.Log("teste");
            StartCoroutine ("GivePointLinha");
        }

        if (collision.gameObject.CompareTag("Mar"))
        {
            StartCoroutine("GivePointMar");
        }

        if (collision.gameObject.CompareTag("Paredes"))
        {
            StartCoroutine("SomParede");
        }

        if (collision.gameObject.CompareTag("Bomba"))
        {
            StartCoroutine("SomParede");
        }

        if (collision.gameObject.CompareTag("Ring"))
        {
            StartCoroutine("SomBoia");
        }

    }

 

    IEnumerator GivePointLinha()
    {
        
        if (!Desliga)
        {
            Desliga = true;
            AddScore(PontosLinha);
        }
        yield return new WaitForSecondsRealtime (0.2f);
        Desliga = false;


    }

    IEnumerator GivePointRing()
    {

        if (!Desliga)
        {
            Desliga = true;
            AddScore(PontosArco);
          
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator GivePointMar()
    {

        if (!Desliga)
        {
            Desliga = true;
            AddScore(PontosAgua);
     
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator GivePointBomb()
    {

        if (!Desliga)
        {
            Desliga = true;
            AddScore(PontosBomba);
       
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator SomParede()
    {

        if (!Desliga)
        {
            Desliga = true;
          
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;


    }

    IEnumerator SomBoia()
    {

        if (!Desliga)
        {
            Desliga = true;
  

        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;


    }


    public void ShowHighScore()
    {
        
        highscore.SetActive(true);
    }

    public void HideHighScore()
    {
       
        highscore.SetActive(false);
    }

}
