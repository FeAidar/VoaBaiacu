using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSettings : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb2D;
    public Rigidbody2D Rigidbody2D => rb2D;
    [SerializeField] private FishHitAnimations animator;
    [FormerlySerializedAs("soundEmitter")] [SerializeField] private MovableSoundEmitter movableSoundEmitter;
    private GameSettingsSO _gameSettingsSo;
    private int _currentLives;
    private bool _immortal;
    public int CurrentLives => _currentLives;
    
    

    private void Awake()
    {
        GameManager.OnStartGame += StartGame;
        GameManager.OnEndGame += Stop;
        GameManager.OnParseSettings += GetSettings;
    }

    private void OnDestroy()
    {
        GameManager.OnParseSettings -= GetSettings;
        GameManager.OnStartGame -= StartGame;
        GameManager.OnEndGame -= Stop;
    }

    private void GetSettings(GameSettingsSO settings)
    {
        _gameSettingsSo = settings;
        _currentLives = _gameSettingsSo.lifeSettings.playerLife;
        if (_gameSettingsSo.lifeSettings.playerLife == 0)
        {
            _immortal = true;
        }
    }

    private void StartGame()
    {
        movableSoundEmitter.PlayAudio(movableSoundEmitter.AppearAudioClips);
        rb2D.constraints = RigidbodyConstraints2D.None;
        animator.enabled = true;
     
    }

    private void Stop()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.enabled = false;
    }

    private void CheckLives(int DamageTaken)
    {
        if (_immortal) return;
        _currentLives = Mathf.Max(0, _currentLives - DamageTaken);
        //Call action to GameManager (UI, Counter)
       if (_currentLives == 0)
        {
            //call GameOver
        }
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<CoolDownController>(out var cd))
        {
            if (!cd.CanBeTouched()) return;
            if (CheckLine(other.gameObject)) return;
            if (CheckSpawnable(other.gameObject)) return;
            if (CheckWater(other.gameObject)) return;

        }
    }

    private bool CheckSpawnable(GameObject other)
    {
        if (other.TryGetComponent<SpawnableObject>(out var spawnableObject))
        {

          //  spawnableObject.HazardType
        }
        
        return false;
    }

    private bool CheckLine(GameObject other)
    {
        return false;
    }

    private bool CheckWater(GameObject other)
    {
        return false;
    }


    private void Reset()
    {
      
    }


   

}
