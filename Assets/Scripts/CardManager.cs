using System;
using Reuse.CameraControl;
using Reuse.Patterns;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private CardScoreController scoreController;
    [SerializeField] private CardAnimationController cardAnimationController;
    [SerializeField] private LevelDefinition definition;
    [SerializeField] private DragMoveCamera dragCamera;
    [SerializeField] private Transform cardSpawnPoint;
    
    private readonly CardSpawner _spawner = new();
    private readonly CardFacesSpawnController _spawnController = new();
    private readonly CardFacesRevealController _revealController = new();
    
    [SerializeField] private Card cardPrefab;
    private bool _canFlip = false;
    
    public static bool CanFlip => (Instance._canFlip);

    public void StartCardSpawn()
    {
        cardAnimationController.ResetController();
        _spawnController.ResetProcDefinition(definition);
        _revealController.ResetCards();
        scoreController.ResetScore();

        DisableGameFlipAndCamera();
        StartCoroutine(_spawner.SpawnCards(cardSpawnPoint, cardPrefab, definition));
    }

    private void DisableGameFlipAndCamera()
    {
        _canFlip = false;
        dragCamera.enabled = false;
    }

    public void CardAnimationEnd()
    {
        _canFlip = true;
        dragCamera.enabled = true;
    }

    public void CardAnimationStart()
    {
        cardAnimationController.StartAnimation();
        _spawnController.SetAllRemainingVisuals(definition);
    }

    public void CardAnimationAdd(Card card, Vector3 position, Vector3 initialPosition)
    {
        cardAnimationController.AddCard(card, position, initialPosition);
    }

    public void SetBoardBoundaries(float minX, float minY, float maxX, float maxY)
    {
        dragCamera.DefineBounds(minX, minY, maxX, maxY);
    }

    public void SetCardVisual(Card card)
    {
        //The card back image is the same always, but it could change
        _spawnController.AddCardVisual(card, definition);
    }

    public void FlipCard(Card card)
    {
        var res = _revealController.SetFlippedCard(card);
        
        var lose = scoreController.AddScore(res.scoreType);
        
        if (res.gameWon)
        {
            //Logic for win game
            DisableGameFlipAndCamera();
            return;
        }
        
        if(lose)
        {
            //Logic for lose game
            DisableGameFlipAndCamera();
        }
        
        _canFlip = res.canFlipNextCard;
    }

    public void RevertCard(Card card)
    {
        _canFlip = _revealController.SetCardNormalState();
    }

    public void SetCardsAmount(int cardAmount)
    {
        _revealController.SetCardAmount(cardAmount);
        scoreController.SetMaxScore(_revealController.GetCardCombinationAmount());
    }
}