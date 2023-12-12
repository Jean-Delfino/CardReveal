using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilTransform
    {
        public static void SetChildParentToOneLevelAbove(Transform child)
        {
            child.SetParent(child.parent.parent);
        }
        
        public static float GetMiddleCenterHorizontalLayoutGroupPosition(float minValue, float maxValue, int index, int totalOfUpgrades)
        {
            var totalSize = maxValue - minValue;
            var spacing = totalSize / (totalOfUpgrades + 1); //Left and right spacing
            return minValue + ((index + 1) * spacing);
        }
        
        public static void CopyRectTransform(RectTransform destiny, RectTransform origin){
            destiny.anchorMin = origin.anchorMin;
            destiny.anchorMax = origin.anchorMax;
            destiny.anchoredPosition = origin.anchoredPosition;
            destiny.rotation = origin.rotation;
            destiny.sizeDelta = origin.sizeDelta;
            destiny.localScale = origin.localScale;
        }
    }
}