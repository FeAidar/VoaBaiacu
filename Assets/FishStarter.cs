using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStarter : MonoBehaviour
{
   [SerializeField] private Rigidbody2D _rigidbody;
   [SerializeField] private FishHitAnimations _animator;
   [SerializeField] private ScoreGiver _scoreGiver;

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
     _rigidbody.constraints = RigidbodyConstraints2D.None;
     _animator.enabled = true;
     _scoreGiver.enabled = true;
   }

   private void Stop()
   {
      _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
      _animator.enabled = false;
      _scoreGiver.enabled = false;
   }

   private void Reset()
   {
      
   }
}
