using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Storytelling
{
    [CreateAssetMenu(fileName = "StorytellerPhrases", menuName = "StorytellerPhrases", order = 0)]
    public class StorytellerPhrases : ScriptableObject 
    {
        [Serializable]
        public class StoryTellerPhrase
        {
            public string key;
            public PhrasePart[] phrase;
        }

        [Serializable]
        public class PhrasePart
        {
            public int priority;
            public string part;
            public bool isAlternative = false;
        }

        [SerializeField] List<StoryTellerPhrase> phrases;

        public Dictionary<string, PhrasePart[]> GetKeyPhrases(){
            Dictionary<string, PhrasePart[]> keyPhrases = new();

            foreach (var storyTellerPhrase in phrases)
            {
                keyPhrases.Add(storyTellerPhrase.key, storyTellerPhrase.phrase);
            }

            return keyPhrases;
        }
    }
}
