using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public GameObject  _controlador;
    public GameObject blur;
    public GameObject Peixe;
    public GameObject PeixeMorto;
    public GameObject TeladeDerrota;
    
    void Start()
    {
        Time.timeScale = 0;
        

    }

    public void Comeco()
    {
        blur.SetActive(false);
        _controlador.SetActive(true);
        Time.timeScale = 1;
            
    }

    public void Perdeu()
    {
        Time.timeScale = 0;
        PeixeMorto.transform.position = new Vector3(Peixe.transform.position.x, PeixeMorto.transform.position.y);
        PeixeMorto.SetActive(true);
        TeladeDerrota.SetActive(true);
        _controlador.SetActive(false);
    }
    public void Recomeca()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

    }                      
                    

}
