using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayArt : MonoBehaviour
{
   [SerializeField] private GameObject art;
   [SerializeField] private SpritesAndTimes sky;
   [SerializeField] private SpritesAndTimes clouds;
   public void EnterArt()
   {
      if(art.activeSelf) return;
      foreach (SpriteRenderer r in sky.sprites)
      {
        var color = r.color;
        color.a = 0f;
        r.color = color;
      }     
      foreach (SpriteRenderer r in clouds.sprites)
      {
         var color = r.color;
         color.a = 0f;
         r.color = color;
      }
      art.SetActive(true);
      foreach (SpriteRenderer r in sky.sprites)
      {
        r.DOFade(1,Random.Range(sky.entryAnimationDuration.minAnimationDuration,sky.entryAnimationDuration.maxAnimationDuration)).SetDelay(Random.Range(sky.entryAnimationDuration.minAnimationDelay,sky.entryAnimationDuration.maxAnimationDelay));
         
      }
      foreach (SpriteRenderer r in clouds.sprites)
      {
         r.DOFade(Random.Range(clouds.entryAnimationDuration.minAlphaColor, clouds.entryAnimationDuration.maxAlphaColor),Random.Range(clouds.entryAnimationDuration.minAnimationDuration,clouds.entryAnimationDuration.maxAnimationDuration)).SetDelay(Random.Range(clouds.entryAnimationDuration.minAnimationDelay,clouds.entryAnimationDuration.maxAnimationDelay));
         
      }
      
   }
   
   public void ExitArt()
   {
      if (!art.activeSelf) return;
     
      foreach (SpriteRenderer r in clouds.sprites)
      {
         r.DOFade(0,Random.Range(clouds.exitAnimationDuration.minAnimationDuration,clouds.exitAnimationDuration.maxAnimationDuration)).SetDelay(Random.Range(clouds.exitAnimationDuration.minAnimationDelay,clouds.exitAnimationDuration.maxAnimationDelay));
         
      }
      
      foreach (SpriteRenderer r in sky.sprites)
      {
         r.DOFade(0,Random.Range(sky.exitAnimationDuration.minAnimationDuration,sky.exitAnimationDuration.maxAnimationDuration)).SetDelay(Random.Range(sky.exitAnimationDuration.minAnimationDelay,sky.exitAnimationDuration.maxAnimationDelay))
            .OnComplete(() =>
         {
            art.SetActive(false);
         });

      }


       

   }
   
   }

[Serializable]
public struct SpritesAndTimes
{
   public SpriteRenderer[] sprites;
   public AnimationDuration entryAnimationDuration;
   public AnimationDuration exitAnimationDuration;

}
[Serializable]
public struct AnimationDuration
{
   [Range(0,10)]public float minAnimationDuration;
   [Range(0,10)] public float maxAnimationDuration;
   [Range(0,10)]public float minAnimationDelay;
   [Range(0,10)] public float maxAnimationDelay;
   [Range(0, 1)] public float minAlphaColor;
   [Range(0, 1)] public float maxAlphaColor;
}
   

