using System.Collections.Generic;
using UnityEngine;

namespace Reuse.CSV
{
    public static class GameVersatileTextsLocator
    {
        private static VersatileTextsFiles _versatileTextsFilesAsset;
        
        private static readonly Dictionary<string, string[]> Texts = new();

        private static int _actualLanguage = 0; //The values in the array of texts are accessed by language 
        private static int _alternativeLanguage = 0; 
        
        private static bool _hasBeenInitialized;
        
        public static void InitializeTexts(VersatileTextsFiles files)
        {
            if(_hasBeenInitialized) return;
            
            _versatileTextsFilesAsset = files;

            if (Texts.Count == 0)
            {
                ReadAllVersatileFiles();
            }

            _hasBeenInitialized = true;
        }
        
        public static void ChangeActualLanguage(int actualLanguage)
        {
            _actualLanguage = actualLanguage;
        }

        public static void ChangeAlternativeLanguage(int alternativeLanguage)
        {
            _alternativeLanguage = alternativeLanguage;
        }

        public static int GetLanguage(bool isAlternative = false)
        {
            return isAlternative ? _alternativeLanguage : _actualLanguage;
        }

        private static void ReadAllVersatileFiles()
        {
            foreach (var csv in _versatileTextsFilesAsset.files)
            {
                foreach (var csvValues in CommaSeparatedValuesReader.ReadCommaSeparatedFile(csv))
                {
                    Texts.Add(csvValues.Key, csvValues.Value);
                } 
            }
        }

        public static int FindKeyLanguage(string key, string unknownLanguageWord){
            var line = LocalizeLine(key);
            for(var i = 0; i < line.Length; i++)
            {
                if(string.Equals(unknownLanguageWord ,line[i])) return i;
            }

            return -1;
        }

        public static string[] LocalizeLine(string key){
            return !Texts.ContainsKey(key) ? null : Texts[key];
        }

        public static string Localize(string key, bool isAlternative = false)
        {
            return !Texts.ContainsKey(key) ? null : (isAlternative ? Texts[key][_alternativeLanguage] : Texts[key][_actualLanguage]);
        }

        public static bool HasBeenInitialized()
        {
            return _hasBeenInitialized;
        }
    }
}
