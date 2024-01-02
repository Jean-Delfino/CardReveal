using System;
using System.Threading;

namespace Reuse.Utils
{
    public static class UtilLanguage
    {
        public static string GetSystemLanguageCode()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
        
        public static bool CompareLanguagesFirstCode(string language, string toCompareLanguage)
        {
            return string.Equals(
                language.Split('-')[0], 
                toCompareLanguage.Split('-')[0], 
                StringComparison.OrdinalIgnoreCase);
        }
    }
}