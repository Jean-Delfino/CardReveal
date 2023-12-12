using Reuse.CameraControl;
using Reuse.Utils;
using UnityEngine;
using InstanceValues = Reuse.Utils.InstanceValues;

namespace Reuse.ObjectSpawn
{
    public class AbstractProjection : MonoBehaviour
    {
        [SerializeField] private InstanceValues[] instanceValues;

        private MaterialPropertyBlock materialPropertyBlock;

        private RenderIDBlock _ids = new();

        private void Awake()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            _ids.PositionID = Shader.PropertyToID("_Position");
            _ids.RotationID = Shader.PropertyToID("_Rotation");
            _ids.ScaleID = Shader.PropertyToID("_Scale");
        }

        private void Update()
        {
            if(!CameraController.IsMainCameraReady) {
                return;
            }

            UtilRender.Render(instanceValues, materialPropertyBlock, _ids);
        }
    }
}