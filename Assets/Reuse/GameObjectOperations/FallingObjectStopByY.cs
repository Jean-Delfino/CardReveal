using UnityEngine;

namespace Reuse.GameObjectOperations
{

    public class FallingObjectStopByY : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private float fallSpeed = 0.0081f;
        [SerializeField] private  float gravityValue = 9.8f;
        [SerializeField] private float minLimitY = 0f;

        private Vector3 _direction;
        private Vector3 _initialPosition;
        private float _actualTimeElapsed;

        private void Start()
        {
            _actualTimeElapsed = 0;
            _direction = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)).normalized;
            _initialPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            _actualTimeElapsed += Time.deltaTime;
            var elapsedTime = _actualTimeElapsed;
            var x = _direction.x * moveSpeed * elapsedTime;
            var z = _direction.z * moveSpeed * elapsedTime;
            var y = -Mathf.Sqrt(-(fallSpeed * -3.0f * gravityValue * _actualTimeElapsed));
            //var y = (0.5f * -gravity * elapsedTime * elapsedTime) + (speed * elapsedTime);

            var newPosition = new Vector3(_initialPosition.x + x, _initialPosition.y + y, _initialPosition.z + z);
            
            if (transform.position.y <= minLimitY)
            {
                _actualTimeElapsed = 0;
                transform.position = new Vector3(transform.position.x, minLimitY, transform.position.z);
                return;
            }
            
            transform.position = newPosition;
        }
    }
}