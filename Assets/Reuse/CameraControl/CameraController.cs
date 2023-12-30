using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Reuse.Patterns;

namespace Reuse.CameraControl
{
    public enum CameraType
    {
        Default,
        Side,
        AlternativeSide,
    }
    public class CameraController : Singleton<CameraController>
    {
        [SerializeField] private List<GameObject> vCams;

        private Camera _camera;
        private GameObject _actualCamera;

        public static GameObject ActualCamera => Instance._actualCamera;
        public static Quaternion ActualCameraRotation => Instance._actualCamera.transform.rotation;
        public static Vector3 ActualCameraPosition => Instance._actualCamera.transform.position;
        
        public static bool IsMainCameraReady = false;
        private new void Awake()
        {
            base.Awake();

            _camera = Camera.main;
            IsMainCameraReady = true;
            if(vCams.Count > 0) SwitchCamera(CameraType.Default);
        }
        
        public static void SwitchCamera(CameraType cameraType)
        {
            Instance.vCams
                .Where((_, idx) => idx != (int)cameraType)
                .ToList()
                .ForEach(cam =>
                {
                    cam.SetActive(false);
                });

            Instance._actualCamera = Instance.vCams[(int)cameraType];
            Instance._actualCamera.SetActive(true);
        }
        
        public static void InstantaneousSwitchCamera(CameraType cameraType)
        {
            SwitchCamera(cameraType);
            var cameraTransform = Instance._camera.transform;
            
            cameraTransform.rotation = ActualCameraRotation;
            cameraTransform.position = ActualCameraPosition;
        }

        public static void ResetCamera()
        {
            Instance.vCams[(int) CameraType.Default].SetActive(true);
        }

        public static Camera GetMainCamera()
        {
            return Instance._camera;
        }
    }
}

