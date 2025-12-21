using System;
using UnityEngine;
using UnityEngine.Events;


public class SpawnableObject : MonoBehaviour
    {
        protected Spawner spawner;
        [SerializeField] private Renderer mainRenderer;
        [SerializeField] protected float activeDuration;
        [SerializeField] protected Collider2D[] col2D;
        [SerializeField] protected Rigidbody2D rb2D;
        private bool _timedOut = false;
       

 
        
        public Renderer MainRenderer => mainRenderer;
        
        
        private float _timeRemaining;
        
        
        protected virtual void OnEnable()
        {
            _timedOut = false;
            _timeRemaining = activeDuration;
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
        
    }
