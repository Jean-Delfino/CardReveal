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
        { GameState.ReturnToMapSelection, DestroyCardGame },
        { GameState.PlayNextLevel ,PlayNextLevel},
        { GameState.RestartLevel , PlayAgain}
    };

    [SerializeField] private CardEndGameUIController endGameUIController;
    [SerializeField] private CardScoreController scoreController;
    [SerializeField] private CardAnimationController cardAnimationController;
    [SerializeField] private GameObject gameUI;
    
    [SerializeField] private DragMoveCamera dragCamera;
    [SerializeField] private Transform cardSpawnPoint;

    private World _actualWorld;
    private Map _actualMap;
    private LevelDefinition _levelDefinition = null;
    
    private readonly CardSpawner _spawner = new();
    private readonly CardFacesSpawnController _spawnController = new();
    private readonly CardFacesRevealController _revealController = new();
    
    [SerializeField] private Card cardPrefab;
    private bool _canFlip = false;

    [SerializeField] private GameObject blockingMenu;
    public static bool CanFlip => (!Instance.blockingMenu.activeInHierarchy && Instance._canFlip);

    private void Start()
    {
        dragCamera.AddSensibilityModifier(GetCameraSensibility);
    }

    public void SetLevel(Map map, LevelDefinition definition)
    {
        _actualMap = map;
        _levelDefinition = definition;
    }

    public void SetWorld(World world)
    {
        _actualWorld = world;
    }

    private void ResetLevel()
    {
        cardAnimationController.ResetController();
        _spawnController.ResetProcDefinition(_levelDefinition);
        _revealController.ResetCards();
        scoreController.ResetScore();
        DisableGameFlipAndCamera();
        ResetCameraPos();
        gameUI.SetActive(false);
    }

    private void ResetCameraPos()
    {
        if (dragCamera.HasBeenStarted) dragCamera.ResetPos();
    }
    
    private static void StartCardSpawn()
    {
        Instance.ResetLevel();
        Instance.StartCoroutine(Instance._spawner.SpawnCards(Instance.cardSpawnPoint, Instance.cardPrefab, Instance._levelDefinition));
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
        gameUI.SetActive(true);
    }

    public void CardAnimationStart()
    {
        cardAnimationController.StartAnimation();
        _spawnController.SetAllRemainingVisuals(_levelDefinition);
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
        _spawnController.AddCardVisual(card, _levelDefinition);
    }
    
    public void FlipCard(Card card)
    {
        _canFlip  = _revealController.SetFlippedCard(card);
    }

    public void CheckFlippedCards()
    {
        if(!_revealController.HasAllCardsFlipped()) return;
        
        var res = _revealController.CheckFlippedCards();

        _canFlip = res.canFlipRest;
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
    }
    public void SetCardsAmount(int cardAmount)
    {
        _revealController.SetCardAmount(cardAmount);
        scoreController.SetMaxScore(_revealController.GetCardCombinationAmount());
    }

    public void SetStars(int amount)
    {
        UtilCardSave.SetStartAmount(_levelDefinition.GetSaveName(), amount);
    }

    public static void MakeTransition(GameState state)
    {
        transitions[state].Invoke();
    }

    private static void DestroyCardGame()
    {
        Instance.gameUI.SetActive(false);
        Instance._spawnController.DestroyCardGame();
    }

    private static void WrongSetup()
    {
        Debug.LogError("MISSING GAME STATE");
    }

    private static void PlayNextLevel()
    {
        DestroyCardGame();
        (Instance._levelDefinition, Instance._actualMap) = Instance.GetNextLevel();
        StartCardSpawn();
    }

    private static void PlayAgain()
    {
        DestroyCardGame();
        StartCardSpawn();
    }

    public bool HasNextLevel()
    {
        return UtilWorlds.FindNextLevel(_actualWorld,_actualMap, _levelDefinition).level != null;
    }

    public (LevelDefinition level, Map map) GetNextLevel()
    {
        return UtilWorlds.FindNextLevel(_actualWorld, _actualMap, _levelDefinition);
    }

    private float GetCameraSensibility()
    {
        return Instance.blockingMenu.activeInHierarchy ? 0 : UtilCardSave.LoadSensibility();
    }
}