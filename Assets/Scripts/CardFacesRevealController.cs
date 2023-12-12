using System.Collections.Generic;
using System.Linq;

public class CardFacesRevealController
{
    //For our scope this is very simple and OK
    private Card _card1;

    private int _cardAmount;
    private int _correctRevealedCards;

    private readonly HashSet<Card> _alreadySeenCards = new();
    public void ResetCards()
    {
        _card1 = null;
        _correctRevealedCards = 0;
        _alreadySeenCards.Clear();
    }
    
    public (bool canFlipNextCard, bool gameWon, ScoreType scoreType) SetFlippedCard(Card card)
    {
        if (card == _card1) return (true,false, ScoreType.None);

        if (_card1 == null)
        {
            _card1 = card;
            return (true,false, ScoreType.None);
        }

        if (_card1.GetFaceIndex() == card.GetFaceIndex())
        {
            _card1.ShowMatchingCardEffect();
            card.ShowMatchingCardEffect();
            _card1 = null;

            _correctRevealedCards += 2;
            
            return (true, CheckVictory(), FindScoreType(card, true));
        }

        var findScore = FindScoreType(card, false);
        _card1.UnFlipCard();
        card.UnFlipCard();

        _alreadySeenCards.Add(_card1);
        _alreadySeenCards.Add(card);
        
        return (false, false, findScore);
    }
    
    public bool SetCardNormalState()
    {
        if (_card1 != null)
        {
            _card1 = null;
            return false;
        }

        return true;
    }

    public void SetCardAmount(int cardAmount)
    {
        _cardAmount = cardAmount;
    }

    private bool CheckVictory()
    {
        return _correctRevealedCards == _cardAmount;
    }

    private ScoreType FindScoreType(Card card, bool reveal)
    {
        var seen = _alreadySeenCards.Contains(card) || _alreadySeenCards.Contains(_card1);
        
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