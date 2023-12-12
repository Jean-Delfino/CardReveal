using DG.Tweening;
using UnityEngine;

namespace Reuse.UI
{
    public class TranslateEasyTweenComponent : MonoBehaviour
    {
        [Header("ANIMATION SETTINGS")]
        [SerializeField] private float animationTime = 1f;

        [SerializeField] Transform target;

        [Header("ANIMATION VALUES SETTINGS")]
        [SerializeField] private  Ease easeType;
        [SerializeField] private Vector3 targetPosition;

        private Vector3 _stopPosition;
        private bool _isAnimating;
        private Tweener _currentTween;

        private void Awake()
        {
            _stopPosition = target.localPosition;
            _isAnimating = false;
        }

        public void AnimateToPosition()
        {
            AnimateToPosition(targetPosition);
        }

        public void AnimateToPosition(Vector3 desiredPosition)
        {
            if (IsAnimating())
            {
                _currentTween.Kill(); // Interrompe a animação atual se houver uma em andamento
            }

            var currentDistance = Vector3.Distance(target.localPosition, desiredPosition);
            var distance = Vector3.Distance(_stopPosition, desiredPosition);
        
            var currentDuration = distance > 0 ? animationTime * (currentDistance / distance) : animationTime;

            // Inicia a animação para a posição alvo
            _currentTween = target.DOLocalMove(desiredPosition, currentDuration).SetEase(easeType).OnComplete(() =>
            {
                _isAnimating = false;
            });

            _isAnimating = true;
        }

        public bool IsAnimating()
        {
            return _isAnimating && _currentTween != null;
        }

        public void StopAnimation()
        {
            AnimateToPosition(_stopPosition);
        }
    }
}