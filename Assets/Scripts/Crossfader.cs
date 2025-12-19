using System;
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

    }

    public void DoFadeToBlack()
    {
      Fade(Color.black, 1);
      
    }
 
    public void DoFadeToScreen()
    {
        Fade(Color.black, 0);
    }

    public void DoFadeToCustomColor(Color color)
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

