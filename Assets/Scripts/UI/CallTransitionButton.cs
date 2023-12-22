using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CallTransitionButton : MonoBehaviour
    {
        [SerializeField] private CardGameTransitionController.GameState state;

        private void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            CardGameTransitionController.Instance.MakeTransition(state);
        }
    }
}