using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Pattern
{
    public enum CommonEventsName
    {
        Looking,
        Running,
        Hurt,
        Hit,
        Death,
    }
    public class NamedEventPublisher : MonoBehaviour
    {
        public Action<string> generalSubscribers = null;
        public Dictionary<string, Action<string>> specificSubscribers = new();

        public void SendEvent(string eventName)
        {
            generalSubscribers?.Invoke(eventName);

            if(!specificSubscribers.ContainsKey(eventName)) return;

            specificSubscribers[eventName]?.Invoke(eventName);
        }   

        public void SubscribeGeneralSubscriber(Action<string> callback)
        {
            generalSubscribers += callback;
        }

        public void SubscribeSpecificSubscriber(string eventName, Action<string> callback)
        {
            if(!specificSubscribers.ContainsKey(eventName)) specificSubscribers.Add(eventName, null);

            specificSubscribers[eventName] += callback;
        }

        public void UnsubscribeGeneralSubscriber(Action<string> callback)
        {
            generalSubscribers -= callback;
        }

        public void UnsubscribeSpecificSubscriber(string eventName, Action<string> callback)
        {
            if(!specificSubscribers.ContainsKey(eventName)) specificSubscribers.Add(eventName, null);

            specificSubscribers[eventName] -= callback;
        }
    }
}
