using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
   // [SerializeField] private bool usePoolSystem;
    private Camera _camera;
    private GameSettingsSO _settings;
    private TimePeriod _timePeriod;
    private HazardsSO _assignedHazard;
    public HazardsSO AssignedHazard => _assignedHazard;
    private ObjectPool<GameObject> pool;
    
   private void Awake()
    {
        _camera = Camera.main;
        GameManager.OnParseSettings += GetSettings;
        GameManager.OnChangePeriod += GetPeriod;
        GameManager.OnStartGame += Spawn;
        GameManager.OnEndGame += StopSpawn;
    }
   
   public void GetAssignedHazard(HazardsSO assignedHazard)
   {
       _assignedHazard = assignedHazard;
   }
   

    private void OnDestroy()
    {
        GameManager.OnParseSettings -= GetSettings;
        GameManager.OnChangePeriod -= GetPeriod;
        GameManager.OnStartGame -= Spawn;
        GameManager.OnEndGame -= StopSpawn;
    }

    private void GetSettings(GameSettingsSO settings)
    {
        _settings = settings;
        CreatePool();


    }

    private void CreatePool()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    private void GetPeriod(TimePeriod period)
    {
        _timePeriod = period;
    }
    

    void Spawn()
    {
       

    }

    void StopSpawn()
    {
        
    }
    
    private GameObject CreateItem()
    {

        GameObject gameObject = _assignedHazard.hazard;
        gameObject.SetActive(false);
        return gameObject;
    }

    // Called when an item is taken from the pool.
    private void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    // Called when an item is returned to the pool.
    private void OnRelease(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // Called when the pool decides to destroy an item (e.g., above max size).
    private void OnDestroyItem(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    

}



