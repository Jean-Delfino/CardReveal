using System;
using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        
        [Header("TRAVEL VARIABLE")]
        [SerializeField] private  float speed = 5.0f;
        [SerializeField] private  float rotationSpeed = 5.0f;
        [SerializeField] private float followDistance = 3.0f;

        public Action TouchedTarget;
        void Update()
        {
            if(target == null) return;
            var position = transform.position;
            var direction = target.position - position;
            var distance = direction.magnitude;

            if (distance > followDistance)
            {
                Vector3.MoveTowards(position, target.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                return;
            }
            
            TouchedTarget?.Invoke();
        }
    }
}