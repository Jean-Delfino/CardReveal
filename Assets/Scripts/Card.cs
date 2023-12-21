using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardAnimation cardAnimation;
    [SerializeField] private SpriteRenderer backImage;
    [SerializeField] private SpriteRenderer frontImage;
    
    private int _frontCardIndex;
    private bool _mouseDown = false;

    private bool _totallyFlipped = false;

    public bool HasTotallyFlipped => _totallyFlipped;
    public void OnMouseDown()
    {
        _mouseDown = true;
    }

    public void OnMouseUp()
    {
        if (_mouseDown && CardManager.CanFlip)
        {
            CardManager.Instance.FlipCard(this);
            cardAnimation.Flip(true);
        }

        _mouseDown = false;
    }

    public void SetCardArtStyle(Sprite backSprite, Sprite frontSprite)
    {
        backImage.sprite = backSprite;
        frontImage.sprite = frontSprite;
    }

    public void SetBackStyle(Sprite backSprite)
    {
        backImage.sprite = backSprite;
    }
    
    public void SetFrontStyle(Sprite frontSprite, int index)
    {
        frontImage.sprite = frontSprite;
        _frontCardIndex = index;
    }

    public void ShowMatchingCardEffect()
    {
        cardAnimation.ShowMatchingCardAnimation();
    }

    public void UnFlipCard()
    {
        _totallyFlipped = false;
        cardAnimation.Flip(false);
    }

    public void SendFlippedCardToCardManager()
    {
        _totallyFlipped = true;
        CardManager.Instance.CheckFlippedCards();
    }

    public void SendUnFlippedCardToCardManager()
    {
        CardManager.Instance.RevertCard(this);
    }

    public int GetFaceIndex()
    {
        return _frontCardIndex;
    }
}