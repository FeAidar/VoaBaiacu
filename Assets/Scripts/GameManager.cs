using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

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
   
    [Header("Settings")]
    [SerializeField]  private TimePeriod currentPeriod;
    [SerializeField] private float currentPeriodTime;
    [SerializeField] private float currentTotalTime;
    private float _maxPeriodTime;
    
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private GameSettingsSO settings;
    //[SerializeField] private Spawner genericSpawner;
    private bool _canPlay;
    [SerializeField] private List<Spawner> hazards = new List<Spawner>();

     
    [Header("Debug Stuff")]
    public bool ForcePlay;
    public GameObject DebugCamera;
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
           DebugCamera.SetActive(true);
           
        }
        OnStartGame += StartPlaying;
        StartMenu.OnGameStarted += StartSetup;
    }
    

    private void StartPlaying()
    {
        _canPlay = true;
    }

    private void Setup(int period)
    {
        currentPeriod = settings.daySettings[period].period;
       currentPeriodTime = 0f;
        _maxPeriodTime = settings.daySettings[period].duration;

    }
    private void Update()
    {
        if (ForcePlay)
        {
            StartSetup();
          ForcePlay = false;
        }

        if (!_canPlay) return;
        Timer();
        if (currentPeriodTime < _maxPeriodTime) return;
        for (int i = 0; i < settings.daySettings.Length; i++)
        {
            if (settings.daySettings[i].period == currentPeriod)
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
        
        Setup(0);
        CreateSpawners();
        OnParseSettings?.Invoke(settings);
        OnChangePeriod?.Invoke(currentPeriod);
        OnStartGame?.Invoke();
    }

    private void CreateSpawners()
    {
        foreach (var day in settings.daySettings)
        {
            foreach (Hazards hazard in day.hazards)
            {
                var alreadySpawned = false;
                if (hazards.Count > 0)
                {
                    
                    foreach (Spawner currentSpawners in hazards)
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
                            hazards.Add(newObject.GetComponent<Spawner>());
                        }
                    }
              
                }

               
            }
        }
    



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
