using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DayArt : MonoBehaviour
{
   [SerializeField] private GameObject art;
   [SerializeField] private SpriteRenderer[] sky;
   [SerializeField] private SpriteRenderer[] clouds;
   public void EnterArt()
   {
      if(art.activeSelf) return;
      foreach (SpriteRenderer r in sky)
      {
        var color = r.color;
        color.a = 0f;
        r.color = color;
      }     
      foreach (SpriteRenderer r in clouds)
      {
         var color = r.color;
         color.a = 0f;
         r.color = color;
      }
      art.SetActive(true);
      foreach (SpriteRenderer r in sky)
      {
         r.DOFade(1f,1.5f);
         
      }
      foreach (SpriteRenderer r in clouds)
      {
         r.DOFade(Random.Range(0.4f, 0.6f),Random.Range(1.5f, 2f)).SetDelay(Random.Range(0f,1f));
         
      }
      
   }
   
   public void ExitArt()
   {
      if (!art.activeSelf) return;
     
      foreach (SpriteRenderer r in clouds)
      {
         r.DOFade(0f,1.5f).SetDelay(Random.Range(0f,.5f));
         
      }
      
      foreach (SpriteRenderer r in sky)
      {
         r.DOFade(0f, 2f).OnComplete(() =>
         {
            art.SetActive(false);
         });

      }



   }
   
   }
   

