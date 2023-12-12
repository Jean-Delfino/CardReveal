using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private Animator animator;

    private int _spinAmount = 1;
    private float _timeUntilReachDestination = 2.0f;
    private Vector3 _destination = new Vector3(5f, 0f, 0f);
    
    private Quaternion _startRotation;
    private float _totalRotation;
    private float _rotationSpeed;
    private float _elapsedTime = 0f;

    private Vector3 _startPosition;
    
    public bool DoAnimationStep()
    {
        if (_elapsedTime < _timeUntilReachDestination)
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
            
            float t = _elapsedTime / _timeUntilReachDestination;
            
            transform.position = Vector3.Lerp(_startPosition, _destination, t);

            _elapsedTime += Time.deltaTime;
            return true;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, _totalRotation);

        transform.position = _destination;
        
        SetAnimatorState(true);
        
        return false;
    }

    public void SetAnimation(int spinAmount, float animationTime, Vector3 destination, Vector3 initialPosition)
    {
        SetAnimatorState(false);
        
        _spinAmount = spinAmount;
        _timeUntilReachDestination = animationTime;
        _destination = destination;

        _elapsedTime = 0;
        _totalRotation = 180f * spinAmount;
        _rotationSpeed = _totalRotation / animationTime;
        
        _startPosition = transform.position = initialPosition;
    }

    public void Flip(bool state)
    {
        animator.SetBool("Flip", state);
    }

    private void SetAnimatorState(bool state)
    {
        animator.enabled = state;
    }

    public void ShowMatchingCardAnimation()
    {
        animator.SetTrigger("Match");
    }
}
