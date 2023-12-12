using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.CSV
{
    public class GameVersatileTextsController : MonoBehaviour
    {
        [SerializeField] private bool initializeTextsInAwake = true;
        [SerializeField] private VersatileTextsFiles files;
        
        private static VersatileTextsFiles _files;

        private static readonly HashSet<VersatileText> VersatileTexts = new();

        public void Awake()
        {
            if(!initializeTextsInAwake) return;

            CallStartDatabase();
        }

        public void CallStartDatabase(){
            _files = files;
            GameVersatileTextsLocator.InitializeTexts(_files);
            ChangeActualLanguage(files.actualLanguage);
        }

        public static void ChangeActualLanguage(int newLanguage)
        {
            _files.actualLanguage = newLanguage; //This saves the file in memory
            GameVersatileTextsLocator.ChangeActualLanguage(_files.actualLanguage);
            SetAllTexts();
        }

        public static void ChangeAlternativeLanguage(int newLanguage)
        {
            _files.actualAlternativeLanguage = newLanguage; //This saves the file in memory
            GameVersatileTextsLocator.ChangeAlternativeLanguage(_files.actualLanguage);
            SetAllTexts();
        }
        
        private static void SetAllTexts()
        {
            foreach (var text in VersatileTexts)
            {
                text.SetText();
            }
        }

        public static void Subscribe(VersatileText text)
        {
            VersatileTexts.Add(text);
        }
        
        public static void Unsubscribe(VersatileText text)
        {
            VersatileTexts.Remove(text);
        }
    }
}