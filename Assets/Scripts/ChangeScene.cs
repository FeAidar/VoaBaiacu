using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [Header("Level para carregar")]
    [SerializeField] public string level;
    
    protected transicao _transicao;
    public bool sair;

    void Start()
    {
        _transicao = GameObject.FindWithTag("Transicao").GetComponent<transicao>();
    }

    public void Carregalevel()
    {
        StartCoroutine("Carrega", level);
    }

    protected IEnumerator Carrega(string nivel)
    {

 
        if (_transicao != null)
            _transicao.inicia();


        else yield return new WaitForSecondsRealtime(1f);

                
        if (!sair)
            SceneManager.LoadScene(nivel);
        else
        {
           
            Application.Quit();
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
