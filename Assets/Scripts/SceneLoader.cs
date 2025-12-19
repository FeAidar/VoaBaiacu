using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int mainMenuScene;
    [SerializeField] private int gameScene;
    [SerializeField] private int creditsScene;
    [SerializeField] private int[] startingScenes;
    [SerializeField] private Crossfader crossfader;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (int toLoadScene in startingScenes)
        {
           LoadScene(toLoadScene);
        }



        DOVirtual.DelayedCall(.2f, () => crossfader.DoFadeToScreen());
    }

    public void UnloadScene(int scene)
    {
        StartCoroutine(UnLoadYourAsyncScene(scene));
    }

    public void LoadScene(int scene)
    {
        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene loadedScene in scenes)
        {
            if (loadedScene.buildIndex == scene) return;
        }
        StartCoroutine(LoadYourAsyncScene(scene));


    }

    private IEnumerator UnLoadYourAsyncScene(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
        

    }
    

    private IEnumerator LoadYourAsyncScene(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene));


    }
}
