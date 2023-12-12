using System.Collections.Generic;
using UnityEngine.Analytics;

namespace Reuse.Utils
{
    public static class UtilAnalytics
    {
        public static void TriggerAnalyticsEvent(string eventName, Dictionary<string, object> content)
        {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            Analytics.CustomEvent(eventName, content);
#endif
        }
    }
}