using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SelfCheckpointObject : MonoBehaviour
    {
        [SerializeField] private Vector3 returnPosition;

        public void ReturnToCheckpoint()
        {
            transform.localPosition = returnPosition;
        }
    }
}