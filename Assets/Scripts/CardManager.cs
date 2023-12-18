using System;
using System.Collections.Generic;
using Layout;
using Reuse.CameraControl;
using Reuse.Patterns;
using SaveGame;
using UnityEngine;

using GameState = CardGameTransitionController.GameState;

public class CardManager : Singleton<CardManager>
{
    private static Dictionary<GameState, Action> transitions = new Dictionary<GameState, Action>
    {
        { GameState.None, WrongSetup },
        { GameState.MapSelectionToGame, StartCardSpawn },
        { GameState.QuitToMainMenu, DestroyCardGame },
        { GameState.ReturnToMapSelection, DestroyCardGame }
    };

    [SerializeField] private CardEndGameUIController endGameUIController;
    [SerializeField] private CardScoreController scoreController;
    [SerializeField] private CardAnimationController cardAnimationController;

    [SerializeField] private DragMoveCamera dragCamera;
    [SerializeField] private Transform cardSpawnPoint;
    
    private LevelDefinition _definition = null;
    private readonly CardSpawner _spawner = new();
    private readonly CardFacesSpawnController _spawnController = new();
    private readonly CardFacesRevealController _revealController = new();
    
    [SerializeField] private Card cardPrefab;
    private bool _canFlip = false;
    
    public static bool CanFlip => (Instance._canFlip);

    public void SetLevel(LevelDefinition definition)
    {
        _definition = definition;
    }

    private void ResetLevel()
    {
        cardAnimationController.ResetController();
        _spawnController.ResetProcDefinition(_definition);
        _revealController.ResetCards();
        scoreController.ResetScore();
        DisableGameFlipAndCamera();
        ResetCameraPos();
    }

    private void ResetCameraPos()
    {
        if (dragCamera.HasBeenStarted) dragCamera.ResetPos();
    }
    
    private static void StartCardSpawn()
    {
        Instance.ResetLevel();
        Instance.StartCoroutine(Instance._spawner.SpawnCards(Instance.cardSpawnPoint, Instance.cardPrefab, Instance._definition));
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
        _spawnController.SetAllRemainingVisuals(_definition);
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
        _spawnController.AddCardVisual(card, _definition);
    }

    public void FlipCard(Card card)
    {
        var res = _revealController.SetFlippedCard(card);
        
        var lose = scoreController.AddScore(res.scoreType);
        
        if (res.gameWon)
        {
            //Logic for win game
            DisableGameFlipAndCamera();
            scoreController.EndGame(endGameUIController.GetGameUI(true));
            return;
        }
        
        if(lose)
        {
            //Logic for lose game
            DisableGameFlipAndCamera();
            scoreController.EndGame(endGameUIController.GetGameUI(false));
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

    public void SetStars(int amount)
    {
        UtilCardSave.SetStartAmount(_definition.GetSaveName(), amount);
    }

    public static void MakeTransition(GameState state)
    {
        transitions[state].Invoke();
    }

    private static void DestroyCardGame()
    {
        
    }

    private static void WrongSetup()
    {
        Debug.LogError("MISSING GAME STATE");
    }
}