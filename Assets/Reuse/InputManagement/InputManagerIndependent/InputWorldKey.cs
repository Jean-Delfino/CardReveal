using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Reuse.InputManagement.InputManagerIndependent
{
    public class InputWorldKey : MonoBehaviour
    {
        [SerializeField] private string keyName;
        [SerializeField] private bool isNegative;
        [SerializeField] private TextMeshProUGUI textKey;

        void OnEnable()
        {
            textKey.text = InputManager.GetKeyValue(keyName, isNegative);
        }
    }
}
