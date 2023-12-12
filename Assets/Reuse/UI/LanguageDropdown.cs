using System.Collections;
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
        private void Start()
        {
            var languages = GameVersatileTextsLocator.LocalizeLine(languageKey);

            tmpDropdown.ClearOptions();

            var dropdownOptions = new System.Collections.Generic.List<TMP_Dropdown.OptionData>();

            foreach (var option in languages)
            {
                dropdownOptions.Add(new TMP_Dropdown.OptionData(option));
            }

            tmpDropdown.AddOptions(dropdownOptions);

            tmpDropdown.value = GameVersatileTextsLocator.GetLanguage(isAlternative);
            tmpDropdown.onValueChanged.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage(int newLanguage)
        {
            if(isAlternative){
                GameVersatileTextsController.ChangeAlternativeLanguage(newLanguage);
                return;
            }
            
            GameVersatileTextsController.ChangeActualLanguage(newLanguage);
        }   
    }
}