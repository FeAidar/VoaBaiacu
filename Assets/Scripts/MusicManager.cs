using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private SongsSo songList;
    [SerializeField] AudioSource audioSource;
    private AudioClip _currentSong;
    private Tween songTween;

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

    private void OnDestroy()
    {
      
    }

    public void StopSong()
    {
        songTween.Kill();
        songTween= audioSource.DOFade(0f, .2f)
            .OnComplete(() =>
            {
                audioSource.Stop();
            });
    }

    public void StartSong()
    {
        songTween.Kill();
        audioSource.Play();
        songTween = audioSource.DOFade(1f, .2f);
    }

    

    public void PlayCreditsSong()
    {
        PlayWithFade(songList.creditsSong);
    }
    
    
    
    public void PlayIntroSong()
    {
        Debug.Log("AskedForSong");
       PlayWithFade(songList.menuSong);
      
    }
    
    public void  PlayMainGameSong()
    {
       PlayWithFade(songList.mainGameSong);
    }
    
    public void PlayGameOverSong()
    {
      PlayWithFade(songList.gameOverSong);
    }
    
   private void PlayWithFade(AudioClip clip)
    {
        songTween.Kill();
        if (_currentSong == clip)
        {
            audioSource.Play();
            songTween = audioSource.DOFade(1f, .2f);
        }
        else
        {
            songTween= audioSource.DOFade(0f, .2f)
                .OnComplete(() =>
                {
                    audioSource.Stop();
                    audioSource.clip = clip;
                    audioSource.volume = 0f;
                    audioSource.Play();
                    audioSource.DOFade(1f, .2f);
                    _currentSong = clip;
                });
        }
    }
    
    


}
