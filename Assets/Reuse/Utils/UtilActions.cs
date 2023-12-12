using System;

namespace Reuse.Utils
{
    public static class UtilActions
    {
        public static bool InvokeCallbackIfCondition(Action callback, bool condition)
        {
            if(condition) callback?.Invoke(); 
            
            return condition;
        }    
    }
}