using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpriteBasedWaveManager : WavesManager
{
   protected override void SetWaterLayer(WaterType waterType)
   {
      base.SetWaterLayer(waterType);
      foreach (WaterLayer waterLayer in GameSettings.waterLayers)
      {
         if (waterType == waterLayer.type)
         {
            for (int i = 0; i < layers.Length; i++)
            {
               foreach (Renderer render in layers[i].renderers)
               {
                  var r = (SpriteRenderer)render;
                  r.DOColor(waterLayer.colorLayer[i], waterLayer.effectDuration);
               }

               
               
               
            }
            
            
          
   
            
         }
      }
   }
}
