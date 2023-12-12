using System.Collections;
using TMPro;
using UnityEngine;

namespace Reuse.UI
{

    public class FastNumericalTextModifier : MonoBehaviour
    {
        [Space] [Header("ANIMATION CURVE")] [Space]

        public AnimationCurve numberEvaluator;

        public AnimationCurve timeEvaluator;

        [Space] [Header("DURATIONS")] [Space]
        
        private const float MinTime = 0.01f;
        public float maxTime = 1.5f;

        public float delay = 0.15f;

        [Space] [Header("NUMBER LIMITERS")] [Space]

        public float minNumber = 1f;

        public float maxNumber = 100000f;

        public float ReturnActualValue(float actualTime)
        {
            return numberEvaluator.Evaluate(actualTime);
        }

        public float ReturnAnimationTotalTime(float value)
        {
            if (value < minNumber) return -1f;
            var toEvaluate = value < maxNumber ? value : maxNumber;
            return timeEvaluator.Evaluate((toEvaluate / maxNumber ) * maxTime );
        }

        public IEnumerator DoFastPassingNumericalText(TextMeshProUGUI text, string constantInBeginningString,
            int finalValue)
        {
            text.text = SetText(constantInBeginningString, (int) Mathf.Min(minNumber, finalValue));
            var totalAnimationTime = ReturnAnimationTotalTime(finalValue);

            yield return new WaitForSeconds(delay);

            for (var i = MinTime; i < totalAnimationTime; i += Time.deltaTime)
            {
                text.text = SetText(constantInBeginningString,  Mathf.RoundToInt(finalValue * ReturnActualValue(i)));
                yield return null;
            }
            
            text.text = SetText(constantInBeginningString, finalValue);
            yield return null;
        }
        
        public IEnumerator DoFastIncreasingPassingNumericalText(TextMeshProUGUI text, string constantInBeginningString,
            int actualValue, int finalValue)
        {
            text.text = SetText(constantInBeginningString, (int) Mathf.Min(actualValue, finalValue));
            int max, min;

            if (actualValue > finalValue)
            {
                max = actualValue;
                min = finalValue;
            }
            else
            {
                min = actualValue;
                max = finalValue;
            }
            
            var totalAnimationTime = ReturnAnimationTotalTime(max - min);

            yield return new WaitForSeconds(delay);

            for (var i = MinTime; i < totalAnimationTime; i += Time.deltaTime)
            {
                text.text = SetText(constantInBeginningString,  
                    Mathf.RoundToInt(actualValue + ((finalValue - actualValue) * ReturnActualValue(i))));
                
                yield return null;
            }
            
            text.text = SetText(constantInBeginningString, finalValue);
            yield return null;
        }

        private static string SetText(string constantInBeginningString, int value)
        {
            return $"{constantInBeginningString}{value}";
        }
    }


}