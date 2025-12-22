using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


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
        private bool _timedOut = false;
       

 
        
        public Renderer MainRenderer => mainRenderer;
        
        
        private float _timeRemaining;
        
        
        protected virtual void OnEnable()
        {
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
            ReturnToPool();
        }

        protected virtual void ReturnToPool()
        {
            spawner.ReleaseToPool(this.gameObject);
        }

        public virtual void CauseDamage()
        {
            

        }
        
    }
