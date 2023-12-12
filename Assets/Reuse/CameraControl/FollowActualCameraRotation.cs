using UnityEngine;

namespace Reuse.CameraControl
{
    public class FollowActualCameraRotation : MonoBehaviour
    {
        private void Update()
        {
            this.transform.rotation = CameraController.GetMainCamera().transform.rotation;
        }
    }
}