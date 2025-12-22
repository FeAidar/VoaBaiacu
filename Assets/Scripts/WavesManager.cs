using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


public class WavesManager : MonoBehaviour
    {
        protected GameSettingsSO GameSettings;
        [SerializeField] protected WaterLayers[] layers;
        [SerializeField] protected ExtraAssets[] extraAssets;
        [SerializeField] protected float delayToActivateExtras;
      
       
        

        private void Awake()
        {
            GameManager.OnParseSettings += GetSettings;
            WaterManager.OnChangeToxicity += SetWaterLayer;
        }

        private void OnDestroy()
        {
            GameManager.OnParseSettings -= GetSettings;
            WaterManager.OnChangeToxicity -= SetWaterLayer;
        }
        
        private void GetSettings(GameSettingsSO settings)
        {
            GameSettings = settings;
  
        }

        protected virtual void SetWaterLayer(WaterType waterType)
        {
ManageExtraAssets(waterType);
        }

        [Serializable]
        public struct WaterLayers
        {
            public Renderer[] renderers; 
        }

        private void ManageExtraAssets(WaterType waterType)
        {
            List<GameObject> shouldBeEnabled = new List<GameObject>();
            List<GameObject> shouldBeDisabled = new List<GameObject>();
            Vector2 ShowPos=Vector2.zero;
            Vector2 HidingPos = Vector2.zero;
            foreach (var extras in extraAssets)
            {
                if (extras.waterType == waterType)
                {
                    ShowPos = extras.showingPos.position;
                   
                    foreach (var extra in extras.toActivate)
                    {
                        if (!shouldBeEnabled.Contains(extra))
                            shouldBeEnabled.Add(extra);
                        
                    }
                }
                else
                {
                    HidingPos = extras.hidingPos.position;
                    foreach (var extra in extras.toActivate)
                    {
                        if (!shouldBeEnabled.Contains(extra) && !shouldBeDisabled.Contains(extra))
                            shouldBeDisabled.Add(extra);
                    }
                }
            }

            foreach (GameObject extra in shouldBeEnabled)
            {
                if (!extra.activeSelf)
                {
                    extra.transform.position = HidingPos;
                    extra.gameObject.SetActive(true);
                }

                extra.transform.DOMove(ShowPos, 0.3f).SetDelay(delayToActivateExtras);
            }
            foreach (GameObject extra in shouldBeDisabled)
            {
                if (extra.activeSelf)
                    extra.transform.DOMove(HidingPos, 0.3f).OnComplete(()=> extra.gameObject.SetActive(false));
            }

        
                           
                        
                    
                
            
        }
        
        

        [Serializable]
        public struct ExtraAssets
        {
            public WaterType waterType;
            public GameObject[] toActivate;
            public Transform hidingPos;
            public Transform showingPos;

        }
        
    }
    



