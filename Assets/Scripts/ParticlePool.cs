
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private int initialSize = 5;

    private readonly Queue<ParticleSystem> pool = new Queue<ParticleSystem>();

    private void Awake()
    {
        Prewarm();
    }

    private void Prewarm()
    {
        for (int i = 0; i < initialSize; i++)
            CreateNewInstance();
    }
    
    public void Initialize(int size)
    {
        for (int i = 0; i < size; i++)
            CreateNewInstance();
    }

    private ParticleSystem CreateNewInstance()
    {
        ParticleSystem ps = Instantiate(particlePrefab, transform);
        ps.gameObject.SetActive(false);

        var main = ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        var pooled = ps.GetComponent<PooledParticle>();
        if (pooled == null)
            pooled = ps.gameObject.AddComponent<PooledParticle>();

        pooled.SetPool(this);

        pool.Enqueue(ps);
        return ps;
    }

    public ParticleSystem Get()
    {
        if (pool.Count == 0)
            CreateNewInstance();

        ParticleSystem ps = pool.Dequeue();
        ps.gameObject.SetActive(true);
        return ps;
    }

    public void ReturnToPool(ParticleSystem ps)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.gameObject.SetActive(false);
        pool.Enqueue(ps);
    }
}

