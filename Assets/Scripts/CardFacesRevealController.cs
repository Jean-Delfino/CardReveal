using System.Collections.Generic;
using System.Linq;
using Reuse.Sound;
using UnityEngine.SocialPlatforms.Impl;

public class CardFacesRevealController
{
    private const string CardMatchSoundVFX = "card_match";
    //For our scope this is very simple and OK
    private Card _card1;
    private Card _card2;
    
    private int _cardAmount;
    private int _correctRevealedCards;

    private readonly HashSet<Card> _alreadySeenCards = new();
    public void ResetCards()
    {
        _card1 = null;
        _correctRevealedCards = 0;
        _alreadySeenCards.Clear();
    }
    
    public (bool canFlipRest, bool gameWon, ScoreType scoreType) CheckFlippedCards()
    {
        ScoreType score;
        if (_card1.GetFaceIndex() == _card2.GetFaceIndex())
        {
            SoundManager.Instance.PlayAudio(CardMatchSoundVFX);
            
            _card1.ShowMatchingCardEffect();
            _card2.ShowMatchingCardEffect();
            score = FindScoreType(true);
            _card1 = _card2 = null;
            
            _correctRevealedCards += 2;
            
            return (true, CheckVictory(), score);
        }
        
        score = FindScoreType(false);
        
        _alreadySeenCards.Add(_card1);
        _alreadySeenCards.Add(_card2);
        
        _card1.UnFlipCard();
        _card2.UnFlipCard();
        
        _card1 = _card2 = null;

        return (true, false, score);
    }
    
    public bool SetFlippedCard(Card card)
    {
        if (_card1 == null)
        {
            _card1 = card;
            return true;
        }
        
        if (card == _card1) return true;

        _card2 = card;
        
        return false;
    }

    public bool HasAllCardsFlipped()
    {
        return _card1 != null && _card2 != null && _card1.IsTotallyFlipped && _card2.IsTotallyFlipped;
    }

    public void SetCardAmount(int cardAmount)
    {
        _cardAmount = cardAmount;
    }

    private bool CheckVictory()
    {
        return _correctRevealedCards == _cardAmount;
    }

    private ScoreType FindScoreType(bool reveal)
    {
        var seen = _alreadySeenCards.Contains(_card2) || _alreadySeenCards.Contains(_card1);
        
        if (reveal)
        {
            return seen ? ScoreType.MemoryReveal : ScoreType.LuckyReveal;
        }

        return seen ? ScoreType.MemoryMiss : ScoreType.Miss;
    }

    public int GetCardCombinationAmount()
    {
        return _cardAmount / 2;
    }
}