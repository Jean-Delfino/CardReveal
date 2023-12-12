using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Reuse.CSV;
using PhrasePart = Reuse.Storytelling.StorytellerPhrases.PhrasePart;

namespace Reuse.Storytelling
{
    public class StorytellerText : VersatileText
    {
        [SerializeField] private StorytellerPhrases phrasesDefinition;

        [SerializeField] private string prefixKey = "id_storyteller";
        [SerializeField] private string afterPrefixKey = ": ";

        private Dictionary<string, PhrasePart[]> keyPhrases;

        private new void Awake()
        {
            keyPhrases = phrasesDefinition.GetKeyPhrases();

            base.Awake();
        }

        public override void SetText()
        {
            _text.text = GetText(textKey);
        }

        public string GetText(string key){
            string finalString = GameVersatileTextsLocator.Localize(prefixKey, isAlternative);
            finalString += afterPrefixKey;

            foreach (var item in keyPhrases[key])
            {
                finalString += GameVersatileTextsLocator.Localize(item.part, item.isAlternative);
            }

            return finalString;
        }
    }
}
