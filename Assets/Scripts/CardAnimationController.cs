using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using Reuse.Utils;
using UnityEngine;

public class CardAnimationController : MonoBehaviour
{
    private readonly List<CardAnimation> _cards = new();
    
    [Space] [Header("ANIMATION TIME LIMITERS")]
    [SerializeField] private float minTime = 0.3f;

    [SerializeField] private float maxTime = 2.0f;
    [SerializeField] private float maxTimeReductionPerCard = 0.05f;
    [SerializeField] private float perDistanceTime = 0.15f;

    [Space] [Header("SPIN LIMITERS")] [Space] 
    
    [SerializeField] private float spinDistanceDivider = 3.0f;

    [SerializeField] private int maxSpin = 3;
    
    private float _actualMaxTime;

    public void ResetController()
    {
        _actualMaxTime = maxTime;
    }

    public void AddCard(Card card, Vector3 destination, Vector3 initialPosition)
    {
        var cardAnimation = card.GetComponent<CardAnimation>();
        var distance = UtilMathOperations.CalculateEuclideanDistance(destination, initialPosition);
        
        cardAnimation.SetAnimation(DefineSpinAmount(distance), 
            DefineAnimationTime(distance), 
            destination,
            initialPosition);
        
        _cards.Add(cardAnimation);
        
        _actualMaxTime = Mathf.Max(minTime, _actualMaxTime - maxTimeReductionPerCard);
    }

    public void StartAnimation()
    {
        StartCoroutine(AnimateCards());
    }

    private IEnumerator AnimateCards()
    {
        bool running;
        while (_cards.Count > 0)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                running = _cards[i].DoAnimationStep();

                if (!running)
                {
                    _cards.RemoveAt(i);
                    i--;
                }
            }

            yield return null;
        }

        CardManager.Instance.CardAnimationEnd();
    }

    private int DefineSpinAmount(float distance)
    {
        return Mathf.Min(maxSpin, Mathf.FloorToInt(distance / spinDistanceDivider));
    }

    private float DefineAnimationTime(float distance)
    {
        return Mathf.Min(_actualMaxTime, distance * perDistanceTime);
    }
}