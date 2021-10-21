using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
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


    const string bestID = "Score_Best";

    [SerializeField] Text highscore;

    int highScore;
    int scoreValue = 0;
    Text score;
}
