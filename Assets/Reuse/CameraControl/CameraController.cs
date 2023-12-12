using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

namespace Reuse.CameraControl
{
    public enum CameraType
    {
        Default,
        Side,
        AlternativeSide,
    }
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> vCams;

        private static CameraController _instance;

        private Camera _camera;
        private GameObject _actualCamera;


        public static GameObject ActualCamera => _instance._actualCamera;
        public static Quaternion ActualCameraRotation => _instance._actualCamera.transform.rotation;
        public static Vector3 ActualCameraPosition => _instance._actualCamera.transform.position;
        
        public static bool IsMainCameraReady = false;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            
            _camera = Camera.main;
            IsMainCameraReady = true;
            if(vCams.Count > 0) SwitchCamera(CameraType.Default);
        }
        
        public static void SwitchCamera(CameraType cameraType)
        {
            _instance.vCams.Where((_, idx) => idx != (int) cameraType).ForEach(cam =>
            {
                cam.SetActive(false);
            });

            _instance._actualCamera = _instance.vCams[(int)cameraType];
            _instance._actualCamera.SetActive(true);
        }
        
        public static void InstantaneousSwitchCamera(CameraType cameraType)
        {
            SwitchCamera(cameraType);
            var cameraTransform = _instance._camera.transform;
            
            cameraTransform.rotation = ActualCameraRotation;
            cameraTransform.position = ActualCameraPosition;
        }

        public static void ResetCamera()
        {
            _instance.vCams[(int) CameraType.Default].SetActive(true);
        }

        public static Camera GetMainCamera()
        {
            return _instance._camera;
        }
    }
}

