using UnityEngine;
using Reuse.CameraControl;

namespace Reuse.Utils
{
    public static class UtilCamera
    {
        public static Vector3 GetWorldToScreenPosition(Vector3 position)
        {
            return CameraController.GetMainCamera().WorldToScreenPoint(position);
        }

        public static Vector3 GetScreenToWorldPosition(Vector3 position)
        {
            return CameraController.GetMainCamera().ScreenToWorldPoint(position);
        }

        public static Vector3 GetViewportToWorldPoint(Vector3 position)
        {
            return CameraController.GetMainCamera().ViewportToWorldPoint(position);
        }

        public static Vector3 GetWorldToViewportPoint(Vector3 position)
        {
            return CameraController.GetMainCamera().WorldToViewportPoint(position);
        }

        public static (float minX, float minY, float maxX, float maxY) GetSimpleCameraBoundsBasedOnWorldPos(float minX, float minY, float maxX, float maxY)
        {
            var mainCamera = CameraController.GetMainCamera();
            var verticalExtent = mainCamera.orthographicSize;
            var horizontalExtent = verticalExtent * mainCamera.aspect;

            return (minX + horizontalExtent, minY + verticalExtent, maxX - horizontalExtent, maxY - verticalExtent);
        }
    }
}
