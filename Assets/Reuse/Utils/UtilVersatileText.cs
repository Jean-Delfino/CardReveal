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
                    CompareLanguagesFirstCode( languageCode, languages[i])) return i;
            }

            return -1;
        }

        private static bool CompareLanguagesFirstCode(string language, string toCompareLanguage)
        {
            return string.Equals(
                language.Split('-')[0], 
                toCompareLanguage.Split('-')[0], 
                StringComparison.OrdinalIgnoreCase);
        }
    }
}