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

        private Vector2 _currentMinBounds;
        private Vector2 _currentMaxBounds;

        private bool _drag = false;
        public bool Drag => _drag;
        private void Start()
        {
            _resetCamera = CameraController.GetMainCamera().transform.position;
        }

        private void LateUpdate()
        {
            var mainCamera = CameraController.GetMainCamera();

            if (_drag)
            {
                var newMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var cameraTransform = mainCamera.transform;
                var pos = cameraTransform.position + ((newMousePos - _mousePos) * sensibilityControl);

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

            if (Input.GetMouseButton(1)) mainCamera.transform.position = _resetCamera;

        }

        public void DefineBounds(float minX, float minY, float maxX, float maxY)
        {
            var bounds = UtilCamera.GetSimpleCameraBoundsBasedOnWorldPos(minX, minY, maxX, maxY);

            _currentMinBounds = new Vector2(bounds.minX + minBoundIncrease.x, bounds.minY + minBoundIncrease.y);
            _currentMaxBounds = new Vector2(Mathf.Max(0,bounds.maxX + maxBoundIncrease.x), Mathf.Max(0,bounds.maxY + maxBoundIncrease.y));
        }
    }
}