using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    
    public static SceneLoader Instance { get; private set; }
    [SerializeField] private SceneReferences sceneReferences;
    public SceneReferences SceneReferences => sceneReferences;
  

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
       
    }

    private void Start()
    {
        foreach (var toLoadScene in sceneReferences.StartingScenes)
        {
            Debug.Log(toLoadScene.SceneName);
            LoadSceneAsync(toLoadScene);
        }

    }

    public void UnloadScene(LevelSO scene)
    {
        StartCoroutine(UnLoadYourAsyncScene(scene));
    }

    public void LoadSceneAsync(LevelSO scene)
    {
        if (SceneManager.GetSceneByName(scene.SceneName).isLoaded)
            return;

        StartCoroutine(LoadYourAsyncScene(scene));
        
    }
    public void LoadScene(LevelSO scene)
    {
        if (SceneManager.GetSceneByName(scene.SceneName).isLoaded)
            return;

   
        SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
        DOVirtual.DelayedCall(.2f,() =>
        {
            Debug.Log("Asked for Fade");
            CrossFadeEvents.FadeIn();
        });

        
       
    }


    private IEnumerator UnLoadYourAsyncScene(LevelSO scene)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene.SceneName);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
        

    }
    

    private IEnumerator LoadYourAsyncScene(LevelSO sceneSo)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneSo.SceneName, LoadSceneMode.Additive);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
        Scene scene = SceneManager.GetSceneByName(sceneSo.SceneName);
        SceneManager.SetActiveScene(scene);
        DOVirtual.DelayedCall(.2f,() =>
        {
            Debug.Log("Asked for Fade");
            CrossFadeEvents.FadeIn();
        });




    }
}
