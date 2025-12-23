using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    [Header("Life Settings")]
    public LifeSettings lifeSettings;
    [Header("Day Time Config")]
    [InlineSO]
    public DaySettingsSo[]  daySettings;
   
    [Header("Water Config")]
    public PoisonousWater[] poisonousWaters;
    public WaterLayer[]  waterLayers;
    public WaterEffectsPoolEntry[] waterVfxPools;
    [Header("Scores Config")] public ScoreSystem scoreSystem;
    
    
    [Header("Spawning Config")]
    public SpawningTypes[] spawningTypes;
    private void OnValidate()
    {
        scoreSystem.Validate();
    }

}

[System.Serializable]
public struct WaterLayer
{
    public Color[] colorLayer;
    public WaterType type;
    public float effectDuration;
}

[System.Serializable]
public struct LifeSettings
{
    [Tooltip("If life set to 0, player is immortal")]
    [Range(0, 10)] public int playerLife;
    public LifeByWater[] lifeLossByTouchingWater;

}

[System.Serializable]
public struct LifeByWater
{
    public WaterType type;
    [Range(-5,0)]public int lifeDamage;
}



[System.Serializable]
public struct HazardScoreSystem
{
   public Score score;
   public HazardsSO hazard;

}

[System.Serializable]
public struct PowerUpScoreSystem
{
    [Range(100,2000)]public int appearEveryEarnedPoints;
    public PowerUpSO[] powerUps;

}

[System.Serializable]
public struct WaterScoreSystem
{
    public Score score;
    public WaterType waterType;
   // public bool loseEvenOnDeath;

}

[System.Serializable]
public struct ScoreSystem
{
    [Header("Win/Lose Points")]
    public Score onPlayerTouchingLines;
    public WaterScoreSystem[] onPlayerTouchingWater;
    public HazardScoreSystem[] onPlayerTouchingHazards;
    public HazardScoreSystem[] onHazardsTimingOut;
    public HazardScoreSystem[] onHazardsTouchingWater;
   
   [Header("PowerUp Score Needed")]
   public PowerUpScoreSystem[] powerUpScoreNeeded;
    
    public void Validate()
    {
        object validated = RecursiveValidator.ValidateObject(this);
        this = (ScoreSystem)validated;
        for (int i = 0; i < powerUpScoreNeeded.Length; i++)
        {
            powerUpScoreNeeded[i].appearEveryEarnedPoints =Mathf.RoundToInt(  powerUpScoreNeeded[i].appearEveryEarnedPoints / 50f) * 50;
        }
    

           
        
    }
    
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
public struct WaterEffectsPoolEntry
{
    public WaterType type;
    public ParticlePool pool;
}