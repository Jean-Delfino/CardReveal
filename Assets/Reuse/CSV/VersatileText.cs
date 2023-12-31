using System;
using TMPro;
using UnityEngine;

namespace Reuse.CSV
{
    public class VersatileText : MonoBehaviour
    {
        [SerializeField] protected string textKey;
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] protected bool isAlternative = false;

        [SerializeField] private bool forceCap = false;
        
        public void Awake()
        {
            if(text == null) text = GetComponent<TextMeshProUGUI>();
        }

        private void OnDisable()
        {
            GameVersatileTextsController.Unsubscribe(this);
        }

        private void OnEnable()
        {
            GameVersatileTextsController.Subscribe(this);
            SetText();
        }

        public virtual void SetText()
        {
            SetText(textKey);
        }

        protected void SetText(string key)
        {
            var newText = GameVersatileTextsLocator.Localize(key, isAlternative);
            if(newText == null) return;
            
            text.text = forceCap ? newText.ToUpper() : newText;
        }

        public void DirectlySetText(string phrase){
            text.text = phrase;
        }

        protected void IncreaseText(string key)
        {
            var newText = GameVersatileTextsLocator.Localize(key, isAlternative);
            if(newText == null) return;
            
            text.text += newText;
        }

        public virtual void SetKey(string key){
            textKey = key;
        }

        public TextMeshProUGUI GetTextMeshProUGUI()
        {
            return text;
        }
    }
}