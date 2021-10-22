using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    const string bestID = "Score_Best";

    [SerializeField] Text highscore;

    int highScore;
    int scoreValue = 0;
    public int ponto;
    Text score;
    public void AddScore(int addScore)
    {
        scoreValue += addScore;
        score.text = scoreValue.ToString();

        if (scoreValue > highScore)
        {
            highScore = scoreValue;
            PlayerPrefs.SetInt(bestID, highScore);
            ShowHighScore();
        }
    }


    void Start()
    {
        score = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt(bestID, 0);
        ShowHighScore();
    }

    void ShowHighScore()
    {
        highscore.text = "MELHOR PONTUAÇÃO: " + highScore;
    }

    private void Update()
    {

        ShowHighScore();
        AddScore(ponto);
        Debug.Log(PlayerPrefs.GetInt(bestID));
    }

}
