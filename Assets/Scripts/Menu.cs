using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   public GameObject  _controlador;
    public GameObject blur;
    public GameObject Peixe;
    public GameObject PeixeMorto;
    public GameObject TeladeDerrota;
    public Animator PeixeMenu;
    public Animator Titulo;
    public GameObject comeco;
    public AudioSource StartSound;
    public AudioSource DieSound;
   
    
    void Start()
    {
        Time.timeScale = 0;
        

    }

    public void Comeco()
    {
        Titulo.SetTrigger("comeca");
        StartCoroutine("Animacao");
    }
    public IEnumerator Animacao()
    {
        
                    PeixeMenu.SetTrigger("comeca");
       
        //Wait until we enter the current state
        while (!PeixeMenu.GetCurrentAnimatorStateInfo(0).IsName("PeixeMenu"))
            {
            
            StartSound.Play();
            yield return null;
            }

            //Now, Wait until the current state is done playing
            while ((PeixeMenu.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
            {
            
            blur.SetActive(false);
            yield return null;
            }

            //Done playing. Do something below!
  
        _controlador.SetActive(true);
        comeco.SetActive(false);
        Time.timeScale = 1;
            
    }

    public void Perdeu()
    {
        Time.timeScale = 0;
        Peixe.GetComponent<ScoreGiver>().RegisterScore();
        DieSound.Play();
        PeixeMorto.transform.position = new Vector3(Peixe.transform.position.x, PeixeMorto.transform.position.y);
        PeixeMorto.SetActive(true);
        TeladeDerrota.SetActive(true);
        _controlador.SetActive(false);
        
    }
               
                    

}
