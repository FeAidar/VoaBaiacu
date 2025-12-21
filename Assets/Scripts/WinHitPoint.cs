using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinHitPoint : MonoBehaviour
    {
        [SerializeField] private Collider2D winCollider;
        [SerializeField] private SpawnableObject spawnableObject;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerSettings playerSettings))
            {
                winCollider.enabled = false;
                spawnableObject.Victory();

            }
        }
        
    }
}