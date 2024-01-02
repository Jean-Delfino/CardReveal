using Reuse.Utils;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SendCheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private string triggerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            var player = UtilGameObject.FindParentWithTag(other.gameObject, triggerTag);
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