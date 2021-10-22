using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transicao : MonoBehaviour
{
    [SerializeField] private Animator anima;
    public bool ParaTempo = false;

    public void inicia()
    {
        anima.SetTrigger("Apaga");
        if (ParaTempo)
        {
            Time.timeScale = 0;
        }
    }
    public void finaliza()
    {
        anima.SetTrigger("Acende");
        if (ParaTempo)
        {
            Time.timeScale = 1;
        }
    }
}