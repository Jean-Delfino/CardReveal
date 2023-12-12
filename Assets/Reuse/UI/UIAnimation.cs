using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reuse.UI
{
    public class UIAnimation : MonoBehaviour
    {
        [SerializeField] private UIAnimationInfo onEnableAnimation;
        [SerializeField] private UIAnimationInfo onDisableAnimation;
        [SerializeField] private UIAnimationInfo idleAnimation;

        private List<Image> _objectImages;
        private List<TextMeshProUGUI> _objectTexts;
        

        private void Awake()
        {
            SetupComponents();
        }

        private void SetupComponents()
        {
            _objectImages = new();
            _objectTexts = new();
            var images = GetComponentsInChildren<Image>();
            var texts = GetComponentsInChildren<TextMeshProUGUI>();
            
            if (images.Length > 0) _objectImages.AddRange(images);
            if (texts.Length > 0) _objectTexts.AddRange(texts);
        }

        private void OnEnable()
        {
            var enableAnimationDuration = 0f;
            if (onEnableAnimation != null)
            {
                enableAnimationDuration = ProcessAnimation(onEnableAnimation);
            }

            StartCoroutine(StartIdleAnimation(enableAnimationDuration));
        }

        public float Disable()
        {
            KillAllTweens();
            return ProcessAnimation(onDisableAnimation);
        }

        private void KillAllTweens()
        {
            DOTween.Kill(this.transform);
            foreach (var image in _objectImages)
            {
                DOTween.Kill(image);
            }

            foreach (var text in _objectTexts)
            {
                DOTween.Kill(text);
            }
        }

        private IEnumerator StartIdleAnimation(float enableAnimationDuration)
        {
            yield return new WaitForSeconds(enableAnimationDuration);
            ProcessAnimation(idleAnimation);
        }

        private float ProcessAnimation(UIAnimationInfo info)
        {
            if (info.animationTypes.HasFlag(UIAnimationTypes.None)) return 0;
            var durations = new List<float> { 0 };
            if (info.animationTypes.HasFlag(UIAnimationTypes.Slide)) durations.Add(ApplySlideAnimation(info));
            if (info.animationTypes.HasFlag(UIAnimationTypes.Scale)) durations.Add(ApplyScaleAnimation(info));
            if (info.animationTypes.HasFlag(UIAnimationTypes.Fade)) durations.Add(ApplyFadeAnimation(info));

            return durations.Max();
        }

        private float ApplyFadeAnimation(UIAnimationInfo info)
        {
            var fadeProperties = info.fadeProperties;
            foreach (var image in _objectImages)
            {
                var imageColor = image.color;
                imageColor.a = fadeProperties.startValue;
                image.color = imageColor;
                image.DOFade(fadeProperties.endValue, fadeProperties.duration)
                    .SetEase(fadeProperties.animationEase)
                    .SetDelay(fadeProperties.delay)
                    .SetLoops(fadeProperties.infiniteLoops ? -1 : fadeProperties.loops, fadeProperties.loopType)
                    .SetUpdate(true);
            }

            foreach (var text in _objectTexts)
            {
                var textColor = text.color;
                textColor.a = fadeProperties.startValue;
                text.color = textColor;
                text.DOFade(fadeProperties.endValue, fadeProperties.duration)
                    .SetEase(fadeProperties.animationEase)
                    .SetDelay(fadeProperties.delay)
                    .SetLoops(fadeProperties.infiniteLoops ? -1 : fadeProperties.loops, fadeProperties.loopType)
                    .SetUpdate(true);
            }

            return GetHighestTweenDuration(fadeProperties);
        }

        private float ApplyScaleAnimation(UIAnimationInfo info)
        {
            var scaleProperties = info.scaleProperties;
            transform.localScale = Vector3.one * scaleProperties.initialScale;
            transform.DOScale(scaleProperties.scaleMultiplier, scaleProperties.duration)
                .SetEase(scaleProperties.animationEase)
                .SetDelay(scaleProperties.delay)
                .SetLoops(scaleProperties.infiniteLoops ? -1 : scaleProperties.loops, scaleProperties.loopType)
                .SetUpdate(true);
            return GetHighestTweenDuration(scaleProperties);
        }

        private float ApplySlideAnimation(UIAnimationInfo info)
        {
            var slideProperties = info.slideProperties;
            var rectTransform = transform as RectTransform;
            if (rectTransform == null) return 0;
            
            rectTransform.anchoredPosition = slideProperties.startPosition;
            rectTransform.DOAnchorPos(slideProperties.endPosition, slideProperties.duration)
                .SetEase(slideProperties.animationEase)
                .SetDelay(slideProperties.delay)
                .SetLoops(slideProperties.infiniteLoops ? -1 : slideProperties.loops, slideProperties.loopType)
                .SetUpdate(true);
            return GetHighestTweenDuration(slideProperties);
        }

        private float GetHighestTweenDuration(UIAnimationProperties properties)
        {
            return properties.delay + properties.duration;
        }
    }

    [Flags]
    public enum UIAnimationTypes
    {
        None = 0x1,
        Slide = 0x2,
        Scale = 0x4,
        Fade = 0x8
    }

    [Serializable]
    public class UIAnimationProperties
    {
        public float duration = 0.5f;
        public float delay = 0.2f;
        public int loops = 0;
        public bool infiniteLoops = false;
        public LoopType loopType;
        public Ease animationEase = Ease.Linear;
    }
    
    [Serializable]
    public class UISlideAnimationProperties : UIAnimationProperties
    {
        public Vector3 startPosition;
        public Vector3 endPosition;
    }

    [Serializable]
    public class UIScaleAnimationProperties : UIAnimationProperties
    {
        public float initialScale = 0f;
        public float scaleMultiplier = 1.25f;
    }

    [Serializable]
    public class UIFadeAnimationProperties : UIAnimationProperties
    {
        public float startValue = 0f;
        public float endValue = 0f;
    }

    [Serializable]
    public class UIAnimationInfo
    {
        public UIAnimationTypes animationTypes;

        [ConditionalField(nameof(animationTypes), false, UIAnimationTypes.Slide)]
        public UISlideAnimationProperties slideProperties;
        [ConditionalField(nameof(animationTypes), false, UIAnimationTypes.Scale)]
        public UIScaleAnimationProperties scaleProperties;
        [ConditionalField(nameof(animationTypes), false, UIAnimationTypes.Fade)]
        public UIFadeAnimationProperties fadeProperties;
    }
}
