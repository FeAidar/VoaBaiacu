using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
   public GameObject  _controlador;
    public GameObject blur;
    void Start()
    {
        Time.timeScale = 0;
        

    }

    public void apertou()
    {
        blur.SetActive(false);
        _controlador.SetActive(true);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    
    }

}
