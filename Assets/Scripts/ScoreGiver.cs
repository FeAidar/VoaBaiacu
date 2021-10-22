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
    public List<AudioSource> SonsBatidas = new List<AudioSource>();
    public AudioSource BatidaAgua;
    public AudioSource BombaExplode;
    public AudioSource Boia;



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
            highScore = scoreValue;
            PlayerPrefs.SetInt(bestID, highScore);
            ShowHighScore();
        }
    }

    // Update is called once per frame
    void Update()
    {

        


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

    }

    IEnumerator GivePointLinha()
    {
        
        if (!Desliga)
        {
            Desliga = true;
            SonsBatidas[Random.Range(0, (SonsBatidas.Count - 1))].Play();
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
            Boia.Play();
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
            BatidaAgua.Play();
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
            BombaExplode.Play();
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;

    }

    IEnumerator SomParede()
    {

        if (!Desliga)
        {
            Desliga = true;
            SonsBatidas[Random.Range(0, (SonsBatidas.Count - 1))].Play();
           
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Desliga = false;


    }


    public void ShowHighScore()
    {
        score.text = "" + highScore;
        highscore.SetActive(true);
    }

}
