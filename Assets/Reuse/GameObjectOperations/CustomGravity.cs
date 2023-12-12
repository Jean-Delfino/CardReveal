using UnityEngine;

namespace Reuse.GameObjectOperations
{

    [RequireComponent(typeof(Rigidbody))]
    public class CustomGravity : MonoBehaviour
    {
        [SerializeField] private float gravityScale = 1f;

        private Rigidbody _rb;
        private const float GravityAcceleration = 9.81f; // Gravidade padrão em m/s^2

        public float GravityScale{
            get => gravityScale;
            set => gravityScale = value;
        }

        public void StopMovement(){
            _rb.velocity = Vector3.zero;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false; // Desativa a gravidade padrão do Rigidbody
        }

        private void FixedUpdate()
        {
            ApplyCustomGravity();
        }

        private void ApplyCustomGravity()
        {
            Vector3 customGravityDirection = Vector3.down; // Define a direção da gravidade personalizada (para baixo)
            Vector3 customGravity = customGravityDirection * GetGravity();
            _rb.AddForce(customGravity, ForceMode.Acceleration);
        }

        public float GetGravity(){
            return gravityScale * GravityAcceleration;
        }
    }
}

