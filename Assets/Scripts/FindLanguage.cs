using System;
using System.Collections;
using Reuse.CSV;
using Reuse.Utils;
using SaveGame;
using UnityEngine;

public class FindLanguage : MonoBehaviour
{
    [SerializeField] private string languageKey = "id_language_code";
    private void Start()
    {
        if (UtilCardSave.LoadLanguage() > -1) return;
        
        StartCoroutine(WaitAndSetLanguage());
    }

    private IEnumerator WaitAndSetLanguage()
    {
        yield return new WaitUntil(GameVersatileTextsLocator.HasBeenInitialized);
        var language = UtilGameVersatileText.FindComputerLanguageConsiderOnlyLanguageToo(languageKey);
        if(language == -1) yield break;
        
        GameVersatileTextsController.ChangeActualLanguage(language);
        UtilCardSave.SaveLanguage(language);
        yield return null;
    }
}