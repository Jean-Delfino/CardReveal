using UnityEngine;

namespace Reuse.Checkpoint
{
    public abstract class TeleportTarget : MonoBehaviour
    {
        public abstract void Teleport(Vector3 teleportPosition);
    }
}