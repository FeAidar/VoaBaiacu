using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private GameSettingsSO _settings;
    private TimePeriod _timePeriod;
    private HazardsSO _assignedHazard;
    public HazardsSO AssignedHazard => _assignedHazard;
    protected ObjectPool<GameObject> _pool;
    [SerializeField]private int _poolCapacity = 10;
    protected float _minWaitTime;
    protected float _maxWaitTime;
    protected float _delayStartTime;
    protected Vector3 _topRight;
    protected Vector3 _bottomLeft;
    protected Coroutine _spawnRoutine;
    
   private void Awake()
    {
        GameManager.OnParseSettings += GetSettings;
        GameManager.OnChangePeriod += GetPeriod;
        GameManager.OnStartGame += SpawnObject;
        GameManager.OnEndGame += StopSpawn;
        ScreenBounds.OnChange += UpdateScreenBounds;
        if (_topRight == Vector3.zero && _bottomLeft == Vector3.zero)
        {
            var bounds = FindObjectOfType<ScreenBounds>();
            if (bounds)
            {
                _topRight = bounds.TopRightCorner;
                _bottomLeft = bounds.DownLeftCorner;
            }
        }
    }
   
    
    private void OnDestroy()
    {
        GameManager.OnParseSettings -= GetSettings;
        GameManager.OnChangePeriod -= GetPeriod;
        GameManager.OnStartGame -= SpawnObject;
        GameManager.OnEndGame -= StopSpawn;
        ScreenBounds.OnChange -= UpdateScreenBounds;
  
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
    }

    // Receives updated screen bounds from ScreenBounds
    private void UpdateScreenBounds(Vector3 topRight, Vector3 bottomLeft)
    {
        _topRight = topRight;
        _bottomLeft = bottomLeft;
    }
    public void GetAssignedHazard(HazardsSO assignedHazard)
    {
        _assignedHazard = assignedHazard;
    }

    private void GetSettings(GameSettingsSO settings)
    {
        _settings = settings;
        SpawnAll();


    }

    private void GetPeriod(TimePeriod period)
    {
        bool alreadyStarted = false;
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            alreadyStarted = true;
        }
        _timePeriod = period;
        foreach (var dayPeriod in _settings.dayPeriods)
        {
            if (dayPeriod.period == period)
            {
                foreach (var hazards in dayPeriod.hazards)
                {
                    if (hazards.hazard == _assignedHazard)
                    {
                        _minWaitTime = hazards.minSpawningRate;
                        _maxWaitTime = hazards.maxSpawningRate;
                        _delayStartTime = hazards.gameStartDelay;
                        if (alreadyStarted)
                        {
                            SpawnObject();
                        }
                    }
                }
            }
        }
      
    }
    
    void SpawnAll()
    {
      CreatePool();
      StartCoroutine(GenerateAllPool());

    }

    protected virtual void  SpawnObject()
    {
      DOVirtual.DelayedCall(_delayStartTime, () =>
      {
          Debug.Log("SpawnObject");
          _spawnRoutine = StartCoroutine(SpawnLoop());
      });
       
    }
    protected virtual IEnumerator SpawnLoop()
    {
        yield return null;
    }
    
    void StopSpawn()
    {
        
    }

    #region PoolingSystem

    // Creates a new pooled GameObject the first time (and whenever the pool needs more).
    private GameObject CreateItem()
    {
        GameObject gameObject =  Instantiate(_assignedHazard.hazard, Vector3.zero, Quaternion.identity);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(this.transform);
        gameObject.GetComponent<SpawnableObject>().GetSpawner(this);
        return gameObject;
    }

    // Called when an item is taken from the pool.
    private void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    void CreatePool()
    {
        // Create a pool with the four core callbacks.
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 25
        );
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

    public void ReleaseToPool(GameObject spawnedObject)
    {
        _pool.Release(spawnedObject);
    }
    
    private IEnumerator GenerateAllPool()
    {
        List<GameObject> checklist = new List<GameObject>();
        while (_pool.CountAll < _poolCapacity)
        {

            GameObject gameObject = _pool.Get();
            checklist.Add(gameObject);

        }

        foreach (var pooledObject in checklist)
        {
            _pool.Release(pooledObject);
        }
       

        yield return  null;
    }

    #endregion
   

}



