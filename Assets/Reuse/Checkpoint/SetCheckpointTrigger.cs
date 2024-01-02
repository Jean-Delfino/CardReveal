using Reuse.Utils;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SetCheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Transform returnPosition;
        [SerializeField] private string triggerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger || UtilGameObject.FindParentWithTag(other.gameObject, triggerTag)) CheckpointManager.SetLastCheckpoint(returnPosition);
        }
    }
}
