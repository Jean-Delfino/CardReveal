using System.Threading;

namespace Reuse.Utils
{
    public static class UtilLanguage
    {
        public static string GetSystemLanguageCode()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}