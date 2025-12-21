using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class StartMenu : MonoBehaviour
{
    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;

    [SerializeField] private Volume backgroundBlur;
    [SerializeField] private AudioSource gameStartSound;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private PlayableDirector introCutscene;
    [SerializeField] private float blurFadeDelay;
    [SerializeField] private float blurFadeSpeed;
    [SerializeField] private float canvasFadeDelay;
    [SerializeField] private float canvasFadeSpeed;
    [SerializeField] private float cutsceneStartDelay;
    [SerializeField] private float timeBeforeStartingGameplay;
    private Tween _blurTween;
    private Tween _canvasTween;
    private bool _gameStarted = false;
    private SceneLoader _sceneLoader;
    [SerializeField] private int sceneToUnload;
    [SerializeField] private int sceneToLoad;

    private void Awake()
    {
        if(!_sceneLoader)
        {_sceneLoader= FindObjectOfType<SceneLoader>();}
        if (_gameStarted) return;
        if (backgroundBlur)
        {
            backgroundBlur.enabled = true;
            backgroundBlur.weight = 1;
        }
     
      
    }

    public void StartButton()
    {
        if (_gameStarted) return;
        _gameStarted = true;
        _sceneLoader.LoadScene(sceneToLoad);
        _blurTween?.Kill();
        _canvasTween?.Kill();
        gameStartSound.Play();
        _canvasTween = mainMenuCanvasGroup.DOFade(0f, canvasFadeSpeed).SetDelay(canvasFadeDelay).OnComplete(() => mainMenuCanvas.enabled = false);
        if (backgroundBlur)
        {
            float intensity = backgroundBlur.weight;
            _blurTween = DOTween.To(x =>intensity = x, intensity, 0, blurFadeSpeed).OnComplete(() => backgroundBlur.enabled=false).OnUpdate(()=>backgroundBlur.weight = intensity);
        }
        DOVirtual.DelayedCall(cutsceneStartDelay,()=> introCutscene.Play()).OnComplete(() => StartCoroutine(WaitForTimelineEnd()));
        
        
        
    }

    private IEnumerator WaitForTimelineEnd()
    {
        while (introCutscene.state == PlayState.Playing)
        {
            yield return null;
            Debug.Log("Playing timeline");
        }
        Debug.Log("Not playing timeline anymore");
        DOVirtual.DelayedCall(timeBeforeStartingGameplay,()=>
        {
            _sceneLoader.UnloadScene(sceneToUnload);
            OnGameStarted?.Invoke();
        });
        
    }
   
    


                    

}


