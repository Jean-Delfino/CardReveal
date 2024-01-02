using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Reuse.InputManagement.InputManagerIndependent
{
    public class ButtonPressGameObjectChangesState : MonoBehaviour
    {
        [SerializeField] private UnityEvent onInactiveGameObject;
        [SerializeField] private UnityEvent onActiveGameObject;
        [SerializeField] private GameObject toCheck;
        
        [SerializeField] private string keyName;
        private void OnEnable()
        {
            StartCoroutine(WaitForKey());
        }

        private IEnumerator WaitForKey()
        {
            while (isActiveAndEnabled)
            {
                if (InputManager.GetAxis(keyName) > 0)
                {
                    DoOperation();
                    yield return new WaitUntil(() => InputManager.GetAxis(keyName) == 0);
                }
                yield return null;
            }

            yield return null;
        }
        
        private void DoOperation()
        {
            if (toCheck.activeSelf)
            {
                onActiveGameObject?.Invoke();
                return;
            }

            onInactiveGameObject?.Invoke();
            
        }
    }
}
