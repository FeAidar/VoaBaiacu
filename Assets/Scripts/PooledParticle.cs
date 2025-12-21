using UnityEngine;

public class PooledParticle : MonoBehaviour
{
    private ParticlePool pool;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void SetPool(ParticlePool particlePool)
    {
        pool = particlePool;
    }

    private void OnParticleSystemStopped()
    {
        if (pool != null)
            pool.ReturnToPool(ps);
    }
}