using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private ScorePanel scorePanel;
    [SerializeField] private LifePanel lifePanel;
    [SerializeField] private PowerUpPanel powerUpPanel;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private Canvas gameOverCanvas;
    private int _startingLives;
    private List<Image> _lives = new List<Image>();


    private void OnEnable()
    {
        ScoreEvents.OnTotalScoreEvent += UpdateScore;
        LifeEvents.OnStartGame +=   GetTotalLives;
        LifeEvents.OnLifeEvent += UpdateLives;
        GameManager.OnStartGame += ShowUI;
        GameManager.OnEndGame += GameOver;
    }

    private void OnDisable()
    {
        ScoreEvents.OnTotalScoreEvent -= UpdateScore;
        LifeEvents.OnStartGame -=   GetTotalLives;
        LifeEvents.OnLifeEvent -= UpdateLives;
        GameManager.OnStartGame -= ShowUI;
        GameManager.OnEndGame -= GameOver;
    }

    private void ShowUI()
    {
        gameOverCanvas.enabled = false;
        canvasGroup.alpha = 0;
        canvas.enabled = true;
        canvasGroup.DOFade(1, 0.5f);

    }

    private void GameOver()
    {
        gameOverCanvas.enabled = true;
    }

    private void GetTotalLives(int value)
    {
        _startingLives = value;
        if (_startingLives > 1)
        {
            lifePanel.iconReference.sprite = lifePanel.lifeIcon;
            lifePanel.iconReference.enabled = false;
            lifePanel.iconReference.gameObject.SetActive(true);
            if(!_lives.Contains(lifePanel.iconReference))
                _lives.Add(lifePanel.iconReference);
            lifePanel.canvas.enabled = true;
            StartCoroutine(InstantiateLife());

        }
        else
            lifePanel.canvas.enabled = false;
        
        
      

    }

    IEnumerator InstantiateLife()
    {
        while (_lives.Count < _startingLives)
        {

            var newIcon = Instantiate(lifePanel.iconReference,lifePanel.canvas.transform);
            _lives.Add(newIcon);
        }

        foreach (Image icon in _lives)
        {
            icon.enabled = true;
            icon.gameObject.SetActive(true);
        }
        UpdateLives(_startingLives);
        yield return null;
    }

    private void UpdateLives(int value)
    {
        if (_startingLives == 1) return;
        if (_startingLives == 0) return;
        for (int i = 0; i < _lives.Count; i++)
        {
            if (i < value)
            {
                if (!_lives[i].gameObject.activeSelf)
                {
                    _lives[i].gameObject.SetActive(true);
                }
                _lives[i].DOFade(1, 0.1f);
            }
            if (i >= value)
            {
                var life =i;
                _lives[life].DOFade(0, 0.1f);
            }
        }
    }

    private void UpdateScore(int value)
    {
        scorePanel.scoreCounter.text = value.ToString();
        scorePanel.highScore.gameObject.SetActive(value >= HighScoreManager.CurrentHighScore);
    }


    [Serializable]
    private struct ScorePanel
    {
        public CanvasGroup canvasGroup;
        public TextMeshProUGUI scoreCounter;
        public TextMeshProUGUI highScore;
    }
    [Serializable]
    private struct LifePanel
    {
        public Canvas canvas;
        public CanvasGroup canvasGroup;
        public Sprite lifeIcon;
        public Image iconReference;
    }
    
    [Serializable]
    private struct PowerUpPanel
    {
        public Canvas canvas;
        public CanvasGroup canvasGroup;
    }
}

