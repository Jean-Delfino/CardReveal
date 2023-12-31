using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Reuse.CSV;

namespace Reuse.UI
{
    public class LanguageDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown tmpDropdown;

        [SerializeField] private string languageKey = "id_language_name";

        [SerializeField] private bool isAlternative = false;
        private void Awake()
        {
            var languages = GameVersatileTextsLocator.LocalizeLine(languageKey);

            tmpDropdown.ClearOptions();

            var dropdownOptions = new List<TMP_Dropdown.OptionData>();

            foreach (var option in languages)
            {
                dropdownOptions.Add(new TMP_Dropdown.OptionData(option));
            }

            tmpDropdown.AddOptions(dropdownOptions);

            tmpDropdown.onValueChanged.AddListener(ChangeLanguage);
        }

        private void OnEnable()
        {
            tmpDropdown.value = GameVersatileTextsLocator.GetLanguage(isAlternative);
        }

        private void ChangeLanguage(int newLanguage)
        {
            if(isAlternative){
                GameVersatileTextsController.ChangeAlternativeLanguage(newLanguage);
                return;
            }
            
            GameVersatileTextsController.ChangeActualLanguage(newLanguage);
        }

        public int GetDropdownValue()
        {
            return tmpDropdown.value;
        }
    }
}