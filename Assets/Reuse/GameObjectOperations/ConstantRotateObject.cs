using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class ConstantRotateObject : MonoBehaviour
    {
        [SerializeField] private float speed = 90.0f;

        [SerializeField] private Vector3 rotateDirection = Vector3.up;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(rotateDirection, speed * Time.deltaTime);
        }
    }
}