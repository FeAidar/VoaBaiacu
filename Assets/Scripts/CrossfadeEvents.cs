using System;
using UnityEngine;

public static class CrossFadeEvents
{
    public static Action OnFadeIn;
    public static Action OnFadeOut;
    public static Action<Color> OnFadeOutColor;

    public static void FadeIn()
    {
        OnFadeIn?.Invoke();
    }
    public static void FadeOut()
    {
        OnFadeOut?.Invoke();
    }

    public static void FadeOut(Color color)
    {
        OnFadeOutColor?.Invoke(color);
    }
    
}