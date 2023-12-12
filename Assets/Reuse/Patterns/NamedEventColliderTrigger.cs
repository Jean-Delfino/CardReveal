using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Pattern
{
    public class NamedEventColliderTrigger : MonoBehaviour
    {
        [SerializeField] private string eventName;
        [SerializeField] private NamedEventPublisher publisher;
        [SerializeField] private string triggerTag = "Player";
        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag(triggerTag))
            {
                publisher.SendEvent(eventName);
            }
        }
    }
}
