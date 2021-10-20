using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaCeu : MonoBehaviour
{
    public float tempo;
    private float _timeRemaining;
    public Animator animator;
    private bool primeiramanhafoi;
    private bool pordosolfoi;
    private bool noitefoi;
    private bool manhafoi;
    public Spawnerdeboia SpawnerDeBoia;
    public Spawnerdeboia SpawnerDeBomba;
    private void Start()
    {
        _timeRemaining = tempo;
      
       // SpawnerDeBoia.TemposManha();
       // SpawnerDeBomba.TemposManha();

    }

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

        }
        else
        {
            _timeRemaining = 0;
          

        }
        if (_timeRemaining < tempo * 0.99)
        {
            if (!primeiramanhafoi)
            {
                SpawnerDeBoia.TemposManha();
                SpawnerDeBomba.TemposManha();
                primeiramanhafoi = true;
            }
         }


        if (_timeRemaining < tempo*0.75)
            {
            if (!pordosolfoi)
            {
                SpawnerDeBomba.TemposTarde();
                SpawnerDeBoia.TemposTarde();
                animator.SetTrigger("PorDoSol");
                pordosolfoi = true;
               
            }

            }

        if (_timeRemaining < tempo * 0.50)
        {
            if (!noitefoi)
            {
                SpawnerDeBoia.TemposNoite();
                SpawnerDeBomba.TemposNoite();
                animator.SetTrigger("Noite");
                noitefoi = true;
            }

        }

        if (_timeRemaining < tempo * 0.25)
        {
            if (!manhafoi)
            {
                SpawnerDeBoia.TemposManha();
                SpawnerDeBomba.TemposManha();
                animator.SetTrigger("NovaManha");
                manhafoi = true;
            }

        }

        if(_timeRemaining == 0)
        {
            _timeRemaining = tempo;
            manhafoi = false;
            pordosolfoi = false;
            noitefoi = false;


        }


    }

}
