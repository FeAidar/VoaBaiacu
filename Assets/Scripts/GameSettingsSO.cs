using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    [Header("Day Time Config")]
    [InlineSO]
    public DaySettingsSo[]  daySettings;
    [Header("Spawning Config")]
    public SpawningTypes[] spawningTypes;
    [Header("Water Config")]
    public PoisonousWater[] poisonousWaters;
    public WaterLayer[]  waterLayers;
    public WaterEffectsPoolEntry[] waterVfxPools;
    [Header("Scores Config")]
    public WaterLayer test;

}

[System.Serializable]
public struct WaterLayer
{
    public Color[] colorLayer;
    public WaterType type;
    public float effectDuration;
}




[System.Serializable]
public struct SpawningTypes
{
    public spawnBounds bounds;
    public Spawner spawnerType;

}

[System.Serializable]
public struct PoisonousWater
{
   
    public WaterType waterType;
    public float duration;

}

public enum WaterType
{ Normal, Toxic, Oil,}

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
    [InlineSO]
    public HazardsSO hazard;
    [Range(0, 60)] public float gameStartDelay;
    [Range(0, 60)]public float minSpawningRate;
    [Range(0, 60)]public float maxSpawningRate;
}

[System.Serializable]
public struct PowerUps
{
    public PowerUpSO powerUp;
    [Range(0, 60)] public float gameStartDelay;
    [Range(0, 60)]public float minSpawningRate;
    [Range(0, 60)]public float maxSpawningRate;
}

[System.Serializable]
public struct WaterEffectsPoolEntry
{
    public WaterType type;
    public ParticlePool pool;
}