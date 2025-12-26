using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Crossfader : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Image canvasImage;
    private Tween _fadeTween;

    private void Awake()
    {

        fader.alpha = 1;
        CrossFadeEvents.OnFadeOut += DoFadeToBlack;
        CrossFadeEvents.OnFadeIn += DoFadeToScreen;
        CrossFadeEvents.OnFadeOutColor += DoFadeToCustomColor;

    }

    private void OnDestroy()
    {
        CrossFadeEvents.OnFadeOut -= DoFadeToBlack;
        CrossFadeEvents.OnFadeIn -= DoFadeToScreen;
        CrossFadeEvents.OnFadeOutColor -= DoFadeToCustomColor;
    }

    private void DoFadeToBlack()
    {
      Fade(Color.black, 1);
      
    }

    private void DoFadeToScreen()
    {
        Fade(Color.black, 0);
    }

    private void DoFadeToCustomColor(Color color)
    {
        Fade(color, 1);
    }
    
    private void Fade(Color color, int value)
    {
        _fadeTween?.Kill();
        if (value == 0)
            canvasImage.color = color;
        _fadeTween = fader.DOFade(value, fadeSpeed);
        
    }

   
}

