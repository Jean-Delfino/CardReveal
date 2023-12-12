using Reuse.Utils;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SetCheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Transform returnPosition;

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger || UtilGameObject.FindParentWithTag(other.gameObject, "Player")) CheckpointManager.SetLastCheckpoint(returnPosition);
        }
    }
}
