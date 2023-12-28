using System;
using System.Collections.Generic;
using Reuse.Utils;
using UnityEngine;

//GameDevBox
//https://www.youtube.com/watch?v=Qd3hkKM-UTI&ab_channel=GameDevBox

namespace Reuse.CameraControl
{
    public class DragMoveCamera : MonoBehaviour
    {
        [SerializeField] private float sensibilityControl = 0.5f;

        private Vector3 _resetCamera;
        private Vector3 _mousePos;

        [SerializeField] private Vector2 minBoundIncrease;
        [SerializeField] private Vector2 maxBoundIncrease;

        [SerializeField] private Vector2 minBoundToMax = Vector2.zero;
        [SerializeField] private Vector2 minBoundToMin = Vector2.zero;

        private Vector2 _currentMinBounds;
        private Vector2 _currentMaxBounds;

        private bool _drag = false;
        public bool Drag => _drag;

        private bool _hasBeenStarted = false;
        public bool HasBeenStarted => _hasBeenStarted;
        
        private readonly HashSet<Func<float>> _multiplySensibility = new();
        private void Start()
        {
            _resetCamera = CameraController.GetMainCamera().transform.position;
            _hasBeenStarted = true;
        }

        private void LateUpdate()
        {
            var mainCamera = CameraController.GetMainCamera();

            if (_drag)
            {
                var newMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var cameraTransform = mainCamera.transform;
                var pos = cameraTransform.position + ((newMousePos - _mousePos) * GetSensibility());

                cameraTransform.position = new Vector3(
                    Mathf.Clamp(pos.x, _currentMinBounds.x, _currentMaxBounds.x),
                    Mathf.Clamp(pos.y, _currentMinBounds.y, _currentMaxBounds.y), transform.position.z);
                
                _mousePos = newMousePos;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _mousePos = (mainCamera.ScreenToWorldPoint(Input.mousePosition));
                _drag = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _drag = false;
            }

            if (Input.GetMouseButton(1)) ResetPos(mainCamera);

        }

        public void ResetPos()
        {
            CameraController.GetMainCamera().transform.position = _resetCamera;
        }
        
        public void ResetPos(Camera mainCamera)
        {
            mainCamera.transform.position = _resetCamera;
        }

        public void DefineBounds(float minX, float minY, float maxX, float maxY)
        {
            var bounds = UtilCamera.GetSimpleCameraBoundsBasedOnWorldPos(minX, minY, maxX, maxY);

            _currentMinBounds = new Vector2(
                Mathf.Min(minBoundToMin.x, bounds.minX + minBoundIncrease.x), 
                Mathf.Min(minBoundToMin.y, bounds.minY + minBoundIncrease.y));
            
            _currentMaxBounds = new Vector2(
                Mathf.Max(minBoundToMax.x,bounds.maxX + maxBoundIncrease.x), 
                Mathf.Max( minBoundToMax.y,bounds.maxY + maxBoundIncrease.y));
        }

        public void AddSensibilityModifier(Func<float> modifier)
        {
            _multiplySensibility.Add(modifier);
        }

        public float GetSensibility()
        {
            var sensibility = sensibilityControl;

            foreach (var func in _multiplySensibility)
            {
                sensibility *= func();
            }

            return sensibility;
        }
    }
}