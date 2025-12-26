using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Solo.MOST_IN_ONE;
using UnityEngine;

public static class ForceFeedBackController
{
    public static Action OnExplosion;
    public static Action OnDamage;



  public static void ShakeHeavy()
    {
        OnExplosion?.Invoke();
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.HeavyImpact);
    }

  public static void ShakeVictory()
    {
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.Success);
    }

    public static void ShakeWaterFall()
    {
       // Debug.Log(MOST_HapticFeedback.HapticsEnabled);
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.LightImpact);
    }

    public static void ShakeBadWater()
    {
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.Failure);
    }

    public static void ShakeLightTouch()
    {
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.SoftImpact);
    }

    public static void ShakeLifeLoss()
    {
        OnDamage?.Invoke();
        MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.Warning);
    }
}
