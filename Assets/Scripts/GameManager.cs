using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    public delegate void ParseSettings(GameSettingsSO settings);
    public static event ParseSettings OnParseSettings;
    public delegate void StartGame();
    public static event StartGame OnStartGame;
    public delegate void ChangePeriod(TimePeriod  period);
    public static event ChangePeriod OnChangePeriod;
    public delegate void EndGame();
    public static event EndGame OnEndGame;
    
    //public delegate void Score(int score);
   // public static event Score OnScoreEvent;
   
    [Header("Settings")]
    [SerializeField] private GameSettingsSO settings;
   
    
   

    [Header("Debug Stuff")]
   // [ShowInInspector, ReadOnly] 
    private List<Spawner> _hazards = new List<Spawner>();
   // [ShowInInspector, ReadOnly] 
    private int _currentScore;
  //  [ShowInInspector, ReadOnly]   
    private TimePeriod _currentPeriod;
  //  [ShowInInspector, ReadOnly] 
    private float _currentPeriodTime;
 //   [ShowInInspector, ReadOnly] 
    private float _currentTotalTime;
    
    private bool _canPlay;
    private float _maxPeriodTime;
    public bool forcePlay;
    public GameObject debugCamera;
    
    
    private void Awake()
    {
        if (Instance != null)
        {
                Destroy(gameObject);
                return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (!Camera.main)
        {
           debugCamera.SetActive(true);
           
        }
        
        ScoreEvents.OnScoreEvent += AddScore;
        LifeEvents.OnGameOverEvent += GameOver;
        
        OnStartGame += StartPlaying;
        StartMenu.OnGameStarted += StartSetup;
    }
    

    private void StartPlaying()
    {
        _canPlay = true;
        _currentTotalTime = 0f;
    }

    private void Setup(int period)
    {
        _currentPeriod = settings.daySettings[period].period;
       _currentPeriodTime = 0f;
        _maxPeriodTime = settings.daySettings[period].duration;

    }
    private void Update()
    {
        if (forcePlay)
        {
            if(!_canPlay)
                 StartSetup(); 
            forcePlay = false;
        }

        if (!_canPlay) return;
        Timer();
        if (_currentPeriodTime < _maxPeriodTime) return;
        for (int i = 0; i < settings.daySettings.Length; i++)
        {
            if (settings.daySettings[i].period == _currentPeriod)
            {
                if (i + 1 < settings.daySettings.Length)
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
        _currentPeriodTime += Time.deltaTime;
        _currentTotalTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        StartMenu.OnGameStarted -= StartSetup;
        OnStartGame -= StartPlaying;
        ScoreEvents.OnScoreEvent -= AddScore;
    }

    private void StartSetup()
    {
        _currentScore = 0;
        Setup(0);
        CreateSpawners();
        OnParseSettings?.Invoke(settings);
        OnChangePeriod?.Invoke(_currentPeriod);
        OnStartGame?.Invoke();
    }

    private void CreateSpawners()
    {
        foreach (var day in settings.daySettings)
        {
            foreach (Hazards hazard in day.hazards)
            {
                var alreadySpawned = false;
                if (_hazards.Count > 0)
                {
                    
                    foreach (Spawner currentSpawners in _hazards)
                    {
                        if (currentSpawners.AssignedHazard == hazard.hazard)
                            alreadySpawned = true;
                    }
                    
                }
                if (!alreadySpawned)
                {
                   
                    foreach (var spawningType in settings.spawningTypes)
                    {
                        if (spawningType.bounds == hazard.hazard.placeToSpawn)
                        {
                            var newObject = Instantiate(spawningType.spawnerType);
                            newObject.name = hazard.hazard.name + " spawner";
                            newObject.GetAssignedHazard(hazard.hazard);
                            _hazards.Add(newObject.GetComponent<Spawner>());
                        }
                    }
              
                }

               
            }
        }
    



    }
    

    private void ChangePeriodOfDay()
    {
        OnChangePeriod?.Invoke(_currentPeriod);
    }

    private void GameOver()
    {
        OnEndGame?.Invoke();
        ScoreEvents.RegisterHighScore(_currentScore);
        _canPlay = false;
    }


    private void AddScore(int value)
    {
        _currentScore = Mathf.Max(0, _currentScore + value);
        ScoreEvents.FullScore(_currentScore);
      
    }

    public void Reset()
    {
        StartSetup();
    }
    
}
