using System.Collections.Generic;
using Layout;
using UnityEngine;
using Reuse.Utils;

public class CardFacesSpawnController
{
    private UtilProbability.ProcDefinition _procDefinition;
    
    private int _cardFaceIndex = -1;

    private readonly List<Card> _toGenerateFaces = new();
    private readonly List<int> _frontFaces = new();
    
    public void ResetProcDefinition(LevelDefinition level)
    {
        _procDefinition = UtilProbability.GenerateProc(level.GetFaceImagesAmount());
        
        UtilProbability.StartDefaultProcChances(_procDefinition);
        
        _toGenerateFaces.Clear();
        _frontFaces.Clear();
    }
    
    public void AddCardVisual(Card card, LevelDefinition level)
    { 
        card.SetBackStyle(GetBackCardImage(level));
        SetFaceCardImage(level);

        _toGenerateFaces.Add(card);
    }

    public void SetAllRemainingVisuals(LevelDefinition level)
    {
        var faceArray = _frontFaces.ToArray();
        UtilRandom.ShuffleArray(ref faceArray);

        for (int i = 0; i < faceArray.Length; i++)
        {
            _toGenerateFaces[i].SetFrontStyle(GetFrontCardImage(level, faceArray[i]), faceArray[i]);
        }
    }

    private Sprite GetBackCardImage(LevelDefinition level)
    {
        return level.GetBackImage();
    }

    private Sprite GetFrontCardImage(LevelDefinition level, int index)
    {
        return level.GetFaceImage(index);
    }
    
    private void SetFaceCardImage(LevelDefinition level)
    {
        if (_cardFaceIndex < 0)
        {
            _cardFaceIndex = UtilProbability.DefaultProcGeneration(_procDefinition);
            _frontFaces.Add(_cardFaceIndex);
            return;
        }
        _frontFaces.Add(_cardFaceIndex);
        _cardFaceIndex = -1;
    }

    public void DestroyCardGame()
    {
        foreach (var card in _toGenerateFaces)
        {
            Object.Destroy(card.gameObject);
        }
    }
}