using Reuse.Utils;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SendCheckpointTrigger : MonoBehaviour
    {
        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            var player = UtilGameObject.FindParentWithTag(other.gameObject, "Player");
            if(player != null)
            {
                player.GetComponent<TeleportTarget>().Teleport(CheckpointManager.GetLastCheckpoint());
                
                return;
            }
            
            var self = other.GetComponent<SelfCheckpointObject>();
            
            if(self != null)
            {
                self.ReturnToCheckpoint();
            }
        }
    }   
}