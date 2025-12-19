using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public DayPeriod[]  dayPeriods;
}

[System.Serializable]
public enum TimePeriod {Morning, Afternoon, Evening}

[System.Serializable]
public struct DayPeriod
{
    public TimePeriod period;
    public float duration;
    public Hazards[] hazards;
}


[System.Serializable]
public struct Hazards
{
    public HazardsSO hazard;
    [Range(0, 60)]public float minSpawningRate;
    [Range(0, 60)]public float maxSpawningRate;
}

