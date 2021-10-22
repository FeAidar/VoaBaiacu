using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    const string bestID = "Score_Best";

  public TMPro.TextMeshProUGUI highscore;

    public int highScore;
    public int scoreValue = 0;
 

    void Start()
    {
       
        highScore = PlayerPrefs.GetInt(bestID, 0);
        ShowHighScore();
    }

    void ShowHighScore()
    {
        highscore.text = "" +highScore;
    }

    private void Update()
    {

        ShowHighScore();
        
    }

}
