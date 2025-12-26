using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;
    
    [Header("Canvas")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private Canvas scorePanel;
    
    [Header("Effects")]
    [SerializeField] private float blurFadeDelay;
    [SerializeField] private float blurFadeSpeed;
    [SerializeField] private float canvasFadeDelay;
    [SerializeField] private float canvasFadeSpeed;
    [SerializeField] private Volume backgroundBlur;
    
    [Header("Sounds")]
    [SerializeField] private AudioSource gameStartSound;
    
   
    [Header("Intro Cutscene Settings")]
    [SerializeField] private PlayableDirector introCutscene;
    [SerializeField] private float cutsceneStartDelay;
    [SerializeField] private float timeBeforeStartingGameplay;


    private Tween _blurTween;
    private Tween _canvasTween;
    private bool _gameStarted = false;

    private void OnEnable()
    {
        SceneReadyNotifier.OnSceneReady += HandleSceneReady;
        if (SceneReadyNotifier.IsSceneReady)
        {
            HandleSceneReady(SceneReadyNotifier.LastReadyScene);
        }
    }
    private void OnDisable()
    {
        SceneReadyNotifier.OnSceneReady -= HandleSceneReady;
    }

    private void HandleSceneReady(string sceneName)
    {
        Debug.Log(sceneName +  "SceneReady");
        if (sceneName != SceneLoader.Instance.SceneReferences.CamerasScene.SceneName) return;
        CanStart();
    }
  

    private  void CanStart()
    {
        if (_gameStarted) return;
      
        if (backgroundBlur)
        {
            backgroundBlur.enabled = true;
            backgroundBlur.weight = 1;
        }

        var score = HighScoreManager.CurrentHighScore;
        if (score < 500)
        {
            scorePanel.enabled =false;
           
        }
        else
        {
            scorePanel.enabled =true;
            highScore.text = HighScoreManager.CurrentHighScore.ToString();
        }

        MusicManager.Instance.PlayIntroSong();
    }

    public void StartButton()
    {
        if (_gameStarted) return;
        _gameStarted = true;
        SceneLoader.Instance.LoadScene(SceneLoader.Instance.SceneReferences.GameScene);
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
            
        }
        MusicManager.Instance.PlayMainGameSong();
        DOVirtual.DelayedCall(timeBeforeStartingGameplay,()=>
        {
           SceneLoader.Instance.UnloadScene(SceneLoader.Instance.SceneReferences.IntroScene);
            OnGameStarted?.Invoke();
        });
        
    }
   
    


                    

}


