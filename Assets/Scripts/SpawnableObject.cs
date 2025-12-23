using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[RequireComponent(typeof(CoolDownController))]

public class SpawnableObject : MonoBehaviour
    {
        protected Spawner spawner;
        protected HazardsSO hazardType;
        public HazardsSO HazardType => hazardType;
        [SerializeField] private Renderer mainRenderer;
        protected float ActiveDuration;
        [SerializeField] protected Collider2D[] col2D;
        [SerializeField] protected Rigidbody2D rb2D;
        public Rigidbody2D Rigidbody2D => rb2D;
        [FormerlySerializedAs("soundEmitter")] [SerializeField] protected MovableSoundEmitter movableSoundEmitter;
        protected bool _timedOut = false;
        [SerializeField] protected WinHitPoint winHitPoint;
        public WinHitPoint WinHitPoint=> winHitPoint;
       

 
        
        public Renderer MainRenderer => mainRenderer;
        
        
       protected float _timeRemaining;
        
        
        protected virtual void OnEnable()
        {
            GameManager.OnEndGame += ReturnToPool;
            _timedOut = false;
            ActiveDuration = hazardType ? hazardType.hazardDuration : 7f;
            _timeRemaining = ActiveDuration;
            ChangeCollidersState(true);
           
        }

       protected void ChangeCollidersState(bool on)
        {
            foreach (var col in col2D)
            {
              
                col.enabled = on;
            }
        }

        private void OnDisable()
        {
            ChangeCollidersState(false);
            GameManager.OnEndGame -= ReturnToPool;
            
        }
        
        public void GetSpawner(Spawner originalSpawner)
        {
            spawner = originalSpawner;
            hazardType = spawner.AssignedHazard;
        }
        
        void Update()
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                return;
            }

            if (_timedOut) return;
            Timeout();
            _timedOut = true;


        }

        protected internal virtual void Victory()
        {
            ReturnToPool();
        }

        protected virtual void Timeout()
        {
            CallScoreByTimeout(hazardType);
            ReturnToPool();
        }

        protected virtual void ReturnToPool()
        {
            spawner.ReleaseToPool(this.gameObject);
        }

        public virtual void CauseDamage()
        {

        }

        protected void CallScoreByTimeout(HazardsSO hazard)
        {
           //call points for timeout
        }        
        
               
        

    }
