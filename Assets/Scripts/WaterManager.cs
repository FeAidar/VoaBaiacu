using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterManager : MonoBehaviour
{
    delegate void ChangeToxicity(WaterType type);
    static event ChangeToxicity OnChangeToxicity;
    
    [Header("Splash Settings")]
    [SerializeField] private float particleHeightOffset = 0.05f;
   
 
    [Header("Sound Settings")]
    [SerializeField] private WaterSplashSoundEmitter soundEmitter;

    [Header("Hazard Reaction Settings")]
    [SerializeField]
    private float activationDelay;
    
    
    private WaterType _currentWaterType;
    private float _timer;
    private Dictionary<WaterType, ParticlePool> pools =
        new Dictionary<WaterType, ParticlePool>();
   
    private GameSettingsSO _settings;
    [SerializeField] private Collider2D col2D;
    private void OnDrawGizmos()
    {
        if (col2D == null)
            return;

        Gizmos.color = Color.cyan;

        Bounds bounds = col2D.bounds;
        float y = bounds.max.y + particleHeightOffset;

        Vector3 left = new Vector3(bounds.min.x, y, 0f);
        Vector3 right = new Vector3(bounds.max.x, y, 0f);

        Gizmos.DrawLine(left, right);
    }

    private void Awake()
    {
        GameManager.OnParseSettings += GetSettings;
    }

    private void OnDestroy()
    {
        GameManager.OnParseSettings -= GetSettings;
    }

    private void GetSettings(GameSettingsSO settings)
    {
        _settings = settings;
        CreatePools();

        ChangeWaterType(WaterType.Water, 1f);
  
    }
    private void CreatePools()
    {
        foreach (var def in _settings.waterVfxPools)
        {
            // instancia como FILHO DA CENA (não do SO)
            ParticlePool pool = Instantiate(def.pool, transform);
            pool.name = $"{def.type}Pool";
            pools.Add(def.type, pool);
        }
    }
    
     private void PlayParticle(WaterType type, Vector3 position, Quaternion rotation)
        {
            ParticlePool pool = GetPool(type);
            if (pool == null)
            {
                Debug.LogWarning($"No pool found for VFX type: {type}");
                return;
            }
            
            ParticleSystem ps = pool.Get();

            // 📍 integração com gameplay
            ps.transform.position = position;
            ps.transform.rotation = rotation;

            ps.Play();
        }

     private ParticlePool GetPool(WaterType type)
        {
            return pools.TryGetValue(type, out var pool) ? pool : null;
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            
            Vector3 spawnPos = GetParticleSpawnPosition(other);
            PlayParticle(_currentWaterType, spawnPos, Quaternion.identity);
            if (other.gameObject.TryGetComponent(out SpawnableObject spawnable))
            {
                
                foreach (var entry in _settings.poisonousWaters)
                {
                    if (entry.hazard== spawnable.HazardType)
                    {
                        soundEmitter.PlayAudio(soundEmitter.DangerWaterAudioClips);
                        ChangeWaterType(entry.waterType, entry.duration);
                        spawnable.CauseDamage();
                    }
                }
            }
            if (other.gameObject.TryGetComponent(out PlayerSettings playerSettings))
            {
                playerSettings.Rigidbody2D.AddForce (transform.up*0.5f, ForceMode2D.Impulse);
                soundEmitter.PlayAudio(soundEmitter.PlayerWaterAudioClips);
            }

            if (other.gameObject.TryGetComponent(out SpawnableObject spawnableObject))
            {
                soundEmitter.PlayAudio(soundEmitter.OtherWaterAudioClips);
                spawnableObject.Rigidbody2D.AddForce (transform.up*0.5f, ForceMode2D.Impulse);
            }
        }
        private Vector3 GetParticleSpawnPosition(Collision2D other)
        {
            // Top of THIS collider (water)
            float topY = col2D.bounds.max.y + particleHeightOffset;

            // X position of the object that entered
            float x = other.collider.bounds.center.x;

            return new Vector3(x, topY, 0f);
        }

        private void ChangeWaterType(WaterType type, float duration)
        {
            _timer = duration;
            _currentWaterType = type;
            DOVirtual.DelayedCall(activationDelay, () => OnChangeToxicity?.Invoke(type));

        }

        private void Update()
        {
            if (_currentWaterType == WaterType.Water) return;
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                ChangeWaterType(WaterType.Water, 1);
            }

        }
}




