using System.Collections;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilPhysics
    {
        public const float DefaultGravity = -9.81f;
        public struct HitDefinition
        {
            public Transform obj;
            public Vector3 contactPoint;
            public Vector3 angle;
            public float strength;
        }
        
        public struct PhysicsApplicationDefinition
        {
            public float duration;
            public LayerMask hitLayer;
            public float gravity;
        }

        public static IEnumerator FlyAndRotate(HitDefinition hit, PhysicsApplicationDefinition definition)
        {
            float elapsedTime = 0.0f;
            var initialPosition = hit.obj.position;
            var zMove = -(hit.obj.forward * hit.strength);
            var xMove = new Vector3(UtilRandom.GetRandomFloatInRange(1, -1), 0, 0) * hit.strength;
            var yMove = Vector3.up * hit.strength;
            
            while (elapsedTime < definition.duration)
            {
                hit.obj.position += 
                    (yMove + zMove + xMove)  * Time.deltaTime;
                yMove += Vector3.up * definition.gravity * Time.deltaTime;
                
                hit.obj.Rotate(hit.angle * hit.strength * Time.deltaTime, Space.Self);

                elapsedTime += Time.deltaTime;

                if(hit.obj.position.y <= initialPosition.y) yield break;
                
                yield return null;
            }
        }
    }
}