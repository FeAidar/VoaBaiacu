using Unity.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerUp", order = 1)]
public class PowerUpSO : ScriptableObject
{
   public GameObject prefab;
   public spawnBounds placeToSpawn;
   public WaterType[] defendsAgainstTypes;
   [Tooltip("If duration is set to 0, PowerUp will not have timer") ]
   [Range(0, 30)] public float powerUpDuration;
   [Range(1, 5)] public int amountOfUses;
}
