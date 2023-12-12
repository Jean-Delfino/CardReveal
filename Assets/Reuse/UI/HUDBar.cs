using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reuse.UI
{
    public class HUDBar : MonoBehaviour
    {
        [SerializeField] protected List<Image> backgroundBars = new();
        [SerializeField] protected Image currentValueBar;
        [SerializeField] protected Image feedbackEffectBar;
        [SerializeField] protected TextMeshProUGUI numbersText;
        [SerializeField] protected float feedbackEffectSpeed = 0.1f;

        private float _maxValue = 100;
        private float _delayedValue = 100;
        private float _currentValue = 100;
        private float _timer = 0;

        private const float _valuesDiffLimit = 0.01f;
        private void Update()
        {
            if(feedbackEffectBar == null) return;

            if (Math.Abs(_delayedValue - _currentValue) > _valuesDiffLimit)
            {
                _delayedValue = Mathf.Lerp(_delayedValue, _currentValue, _timer);
                _timer += feedbackEffectSpeed * Time.deltaTime;
                feedbackEffectBar.fillAmount = _delayedValue / _maxValue;
            }
            else
            {
                _timer = 0;
            }
        }

        public void Initialize(float currentValue, float maxValue)
        {
            _currentValue = currentValue;
            _delayedValue = currentValue;
            _maxValue = maxValue;
            float normalizedValue = _currentValue / _maxValue;
            
            if (currentValueBar != null)
                currentValueBar.fillAmount = normalizedValue;

            if (feedbackEffectBar != null)
                feedbackEffectBar.fillAmount = normalizedValue;

            if (numbersText != null)
                numbersText.text = $"{_currentValue}";
        }

        public void UpdateMaxValue(int newValue)
        {
            _maxValue = newValue;
        }

        public void UpdateCurrentValue(float newValue, bool includeText = true)
        {
            if (currentValueBar == null) return;
            
            _currentValue = newValue > 0 ? newValue : 0;
            
            currentValueBar.fillAmount = _currentValue / _maxValue;
            
            if (includeText)
                numbersText.text = $"{_currentValue}"; // + "/" + _maxValue;
        }

        public void ToggleUI(bool value)
        {
            if (backgroundBars != null)
            {
                foreach (var backgroundBar in backgroundBars)
                {
                    backgroundBar.enabled = value;    
                }
            }
            if (currentValueBar != null) currentValueBar.enabled = value;
            if (feedbackEffectBar != null) feedbackEffectBar.enabled = value;
            if (numbersText != null) numbersText.enabled = value;
            this.gameObject.SetActive(value);
        }
    }
}