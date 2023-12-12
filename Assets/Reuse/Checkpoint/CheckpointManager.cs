using UnityEngine;

namespace Reuse.Checkpoint
{
    public class CheckpointManager : MonoBehaviour
    {
        private static CheckpointManager _instance;

        private Transform _lastCheckpoint;
        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
        }

        public static void SetLastCheckpoint(Transform checkpoint)
        {
            _instance._lastCheckpoint = checkpoint;
        }

        public static Vector3 GetLastCheckpoint()
        {
            return _instance._lastCheckpoint.position;
        }
    }
}
