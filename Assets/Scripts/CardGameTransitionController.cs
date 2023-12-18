using System;
using System.Collections;
using System.Linq;
using Reuse.Patterns;
using UnityEngine;

public class CardGameTransitionController : Singleton<CardGameTransitionController>
{
    public enum GameState
    {
        None,
        MapSelectionToGame,
        ReturnToMapSelection,
        QuitToMainMenu,
    }
    
    [Serializable]
    public class TransitionGame
    {
        public GameState state;
        public string triggerName;

        [Space] [Header("THIS IS BASED ON THE ANIMATOR TIME AND SPEED")]
        public float callManagerTime;
        public float animationSpeed = 1f;
        
        public Animator toTrigger;
    }
    
    [SerializeField] private TransitionGame[] transitions;

    private bool _isInTransition = false;
    
    public void MakeTransition(GameState gameState)
    {
        if(_isInTransition) return;

        StartCoroutine(MakeTransition(transitions.First(e => e.state == gameState)));
    }

    private IEnumerator MakeTransition(TransitionGame transitionGame)
    {
        _isInTransition = true;
        transitionGame.toTrigger.SetTrigger(transitionGame.triggerName);
        yield return new WaitForSeconds(transitionGame.callManagerTime * transitionGame.animationSpeed);
        CardManager.MakeTransition(transitionGame.state);
        _isInTransition = false;
    }
}