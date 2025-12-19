using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ParseSettings(GameSettingsSO settings);
    public static event ParseSettings OnParseSettings;
    public delegate void StartGame();
    public static event StartGame OnStartGame;
    public delegate void ChangePeriod(TimePeriod  period);
    public static event ChangePeriod OnChangePeriod;
    public delegate void EndGame();
    public static event EndGame OnEndGame;
    public bool DEBUGplay;
    
    [SerializeField]  private TimePeriod currentPeriod;
    [SerializeField] private float currentPeriodTime;
    [SerializeField] private float currentTotalTime;
    private float _maxPeriodTime;
    
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private GameSettingsSO settings;
    private bool _canPlay;

    private void Awake()
    {
        OnStartGame += StartPlaying;
        StartMenu.OnGameStarted += StartSetup;
    }
    

    private void StartPlaying()
    {
        _canPlay = true;
    }

    private void Setup(int period)
    {
        currentPeriod = settings.dayPeriods[period].period;
        Debug.Log(currentPeriod);
        currentPeriodTime = 0f;
        _maxPeriodTime = settings.dayPeriods[period].duration;

    }
    private void Update()
    {
        if (DEBUGplay)
        {
            StartSetup();
          DEBUGplay = false;
        }

        if (!_canPlay) return;
        Timer();
        if (currentPeriodTime < _maxPeriodTime) return;
        for (int i = 0; i < settings.dayPeriods.Length; i++)
        {
            if (settings.dayPeriods[i].period == currentPeriod)
            {
                if (i + 1 < settings.dayPeriods.Length)
                {
                    Setup(i+1);
                    ChangePeriodOfDay();
                }
                else
                {
                    Setup(0);
                    ChangePeriodOfDay();
                }

                return;
            }
        }

    }

    private void Timer()
    {
        currentPeriodTime += Time.deltaTime;
        currentTotalTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        StartMenu.OnGameStarted -= StartSetup;
        OnStartGame -= StartPlaying;
    }

    private void StartSetup()
    {
        OnParseSettings?.Invoke(settings);
        Setup(0);
        OnStartGame?.Invoke();
    }

    private void ChangePeriodOfDay()
    {
        OnChangePeriod?.Invoke(currentPeriod);
    }

    private void GameOver()
    {
        OnEndGame?.Invoke();
    }
    
}
