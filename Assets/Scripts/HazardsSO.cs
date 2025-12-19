using UnityEngine;

[CreateAssetMenu(fileName = "Spawnable Object", menuName = "ScriptableObjects/SpawnableObject", order = 1)]


    public class HazardsSO : ScriptableObject
    {
     
            public GameObject hazard;
            public spawnBounds placeToSpawn;
            
    
    }
    [System.Serializable]
    public enum spawnBounds {InsideBounds, OutsideBounds}