using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class UISettingVisualComponent
    {
        [SerializeField] private GameObject on;
        [SerializeField] private GameObject off;

        public void SetComponent(float value)
        {
            bool state = value == 0f;
            
            on.SetActive(!state);
            off.SetActive(state);
        }
    }
}