using Reuse.Patterns;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        private Transform _lastCheckpoint;

        public static void SetLastCheckpoint(Transform checkpoint)
        {
            Instance._lastCheckpoint = checkpoint;
        }

        public static Vector3 GetLastCheckpoint()
        {
            return Instance._lastCheckpoint.position;
        }
    }
}
