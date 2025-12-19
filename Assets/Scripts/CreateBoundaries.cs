using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CreateBoundaries : MonoBehaviour
    {
        [SerializeField] private Transform[] leftBoundary;
        [SerializeField] private Transform[] rightBoundary;
        private Camera _camera;
        private float _lastAspect;
        private void Awake()
        {
            _camera = Camera.main;
            _lastAspect= _camera.aspect;
            SetupBoundaries();
        }

        private void FixedUpdate()
        {
            if (_camera.aspect == _lastAspect) return;
                SetupBoundaries();
                _lastAspect= _camera.aspect;
        }

        private void SetupBoundaries()
        {
            Vector3 newPos = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.nearClipPlane));
            Vector3 newPos2 = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, _camera.nearClipPlane));
            foreach (Transform child in leftBoundary)
            {
                child.position =  new Vector3(newPos.x, child.position.y, child.position.z);
            }

            foreach (Transform child in rightBoundary)
            {
                child.position =  new Vector3(newPos2.x, child.position.y, child.position.z);
            }
        }
    }
}