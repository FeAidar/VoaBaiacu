using System;
using UnityEngine;
    public class CreateBoundaries : MonoBehaviour
    {
        [SerializeField] private Transform[] rightBoundary;
        [SerializeField] private Transform[] leftBoundary;

        private void Awake()
        {
            ScreenBounds.OnChange += SetupBoundaries;
        }

        private void OnDestroy()
        {
           ScreenBounds.OnChange -= SetupBoundaries;
        }
        

        private void SetupBoundaries(Vector3 topLeftCorner, Vector3 downRightCorner)
        {
         
            foreach (Transform child in rightBoundary)
            {
                child.position =  new Vector3(topLeftCorner.x, child.position.y, child.position.z);
            }

            foreach (Transform child in leftBoundary)
            {
                child.position =  new Vector3(downRightCorner.x, child.position.y, child.position.z);
            }
        }
    }
