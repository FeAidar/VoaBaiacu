using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Highscore : MonoBehaviour
{
    public float Score;
    public float highscore;

    public Text scoretext;
    public Text highscoretext;

    public void AddScore()
    {
        Score++;
    }
    void Start()
    {
        highscore = PlayerPrefs.GetFloat("Highscore");
    }

    void Update()
    {
        scoretext.text = Score.ToString();
        highscoretext.text = highscore.ToString();

        if (Score > highscore)
        PlayerPrefs.SetFloat("Highscore", Score);
    }
}
