using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
   [SerializeField] private TimeOfDayArt[] TimeOfDayArt;
   [SerializeField] private float transitionExitDelay;
   private TimePeriod _timePeriod;
   

   
   private void Awake()
   {
       GameManager.OnChangePeriod += CheckDay;
       ForceDay();
      
       

   }
    
   private void OnDestroy()
   {
       GameManager.OnChangePeriod -= CheckDay;
     
  
   }

   private void ForceDay()
   {
       foreach (TimeOfDayArt timeOfDayArt in TimeOfDayArt)
       {
           if (timeOfDayArt.dayPeriod != _timePeriod)
           {
               DOVirtual.DelayedCall(transitionExitDelay, () => timeOfDayArt.dayArt.ExitArt() );
           }

           if (timeOfDayArt.dayPeriod == _timePeriod)
           {
               timeOfDayArt.dayArt.EnterArt();
           }
       }
   }


   private void CheckDay(TimePeriod timePeriod)
   {
       if (timePeriod != _timePeriod)
       {
           foreach (TimeOfDayArt timeOfDayArt in TimeOfDayArt)
           {
               if (timeOfDayArt.dayPeriod != timePeriod)
               {
                   DOVirtual.DelayedCall(transitionExitDelay, () => timeOfDayArt.dayArt.ExitArt() );
               }

               if (timeOfDayArt.dayPeriod == timePeriod)
               {
                   timeOfDayArt.dayArt.EnterArt();
               }
           }
       }
       _timePeriod = timePeriod;
       
   }
}

[System.Serializable]

public struct TimeOfDayArt
{
    public TimePeriod dayPeriod;
    public DayArt dayArt;
    //public SpriteRenderer[] Backgrounds;
    //public SpriteRenderer[] Clouds;
    //public SpriteRenderer[] SecondarySkyElements;
    //public Color defaultWaterColor;
}
