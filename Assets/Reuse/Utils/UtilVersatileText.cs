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
    }
}