using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class WalkAndRotate : MonoBehaviour
    {
        [Header("SPEED AND ANIMATION SETTINGS")]
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float duration = 2f;
        [SerializeField] private float stopTime = 1f;
        
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private Vector3 direction = Vector3.right;
        [SerializeField] private float rotationAngle = 180f;

        [SerializeField] private float speed;

        private int _side = 1;
        private float _currentWalkTime;
        private float _currentStopTime;

        private bool _stopped = false;

        private void Start()
        {
            _currentStopTime = _currentWalkTime = 0;
        }

        private void Update()
        {
            if (_stopped)
            {
                _currentStopTime += Time.deltaTime;
                if (_currentStopTime > stopTime)
                {
                    _currentStopTime = 0;
                    _stopped = false;
                }
                return;
            }
            
            _currentWalkTime += Time.deltaTime;

            if (_currentWalkTime < duration)
            {
                float progress = (_currentWalkTime) / duration;
                transform.position += (direction * progress * speed * Time.deltaTime * _side);
                return;
            }

            _stopped = true;
            _currentWalkTime = 0;
            var rotation = _side == 1 ? rotationAngle : 0;
            _side *= -1;

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}