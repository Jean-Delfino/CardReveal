using System;
using Reuse.CSV;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilGameVersatileText
    {
        public static int FindComputerLanguage(string textControllerKeyLanguage)
        {
            var languageCode = UtilLanguage.GetSystemLanguageCode();
            return GameVersatileTextsLocator.FindKeyLanguage(textControllerKeyLanguage, languageCode);
        }
        
        public static int FindComputerLanguageConsiderOnlyLanguageToo(string textControllerKeyLanguage)
        {
            var languageCode = UtilLanguage.GetSystemLanguageCode();
            var languages = GameVersatileTextsLocator.LocalizeLine(textControllerKeyLanguage);

            for (int i = 0; i < languages.Length; i++)
            {
                if (string.Equals(languageCode, languages[i]) || 
                    UtilLanguage.CompareLanguagesFirstCode( languageCode, languages[i])) return i;
            }

            return -1;
        }
    }
}