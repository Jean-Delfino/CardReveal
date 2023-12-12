using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilTextMeshPro
    {
        [Serializable]
        public class SidedGradient
        {
            public Color defaultColor;

            public ColorMode colorMode;
            public Color topLeft;
            public Color topRight;
            public Color bottomLeft;
            public Color bottomRight;
        }
        
        public static void SetGradient(TextMeshProUGUI text, SidedGradient sidedGradient)
        {
            text.enableVertexGradient = true;
            text.color = sidedGradient.defaultColor;

            TMP_ColorGradient gradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();

            gradient.colorMode = sidedGradient.colorMode;
            gradient.topLeft = sidedGradient.topLeft;
            gradient.topRight = sidedGradient.topRight;
            gradient.bottomLeft = sidedGradient.bottomLeft;
            gradient.bottomLeft = sidedGradient.bottomRight;
            
            text.colorGradientPreset = gradient;
        }
        
        public static void CleanTMProDropdown(TMP_Dropdown tMpDropdown)
        {
            tMpDropdown.ClearOptions();
        }

        public static void AddDropdownOptions(TMP_Dropdown tMpDropdown, IEnumerable<string> options)
        {
            var dropdownOptions = new System.Collections.Generic.List<TMP_Dropdown.OptionData>();

            foreach (var option in options)
            {
                dropdownOptions.Add(new TMP_Dropdown.OptionData(option));
            }

            tMpDropdown.AddOptions(dropdownOptions);
        }
    }
}