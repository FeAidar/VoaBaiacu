using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSettings : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb2D;
    public Rigidbody2D Rigidbody2D => rb2D;
    [SerializeField] private FishHitAnimations animator;
    [SerializeField] private ScoreGiver scoreGiver;
    [FormerlySerializedAs("soundEmitter")] [SerializeField] private MovableSoundEmitter movableSoundEmitter;

    private void Awake()
    {
        GameManager.OnStartGame += StartGame;
        GameManager.OnEndGame += Stop;
    }

    private void OnDestroy()
    {
        GameManager.OnStartGame -= StartGame;
        GameManager.OnEndGame -= Stop;
    }

    private void StartGame()
    {
        movableSoundEmitter.PlayAudio(movableSoundEmitter.AppearAudioClips);
        rb2D.constraints = RigidbodyConstraints2D.None;
        animator.enabled = true;
        scoreGiver.enabled = true;
    }

    private void Stop()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.enabled = false;
        scoreGiver.enabled = false;
    }

    private void Reset()
    {
      
    }


   

}
