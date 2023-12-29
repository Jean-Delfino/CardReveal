using Reuse.Sound;
using UnityEngine;

public enum CardState
{
    None,
    UnFlipped,
    Flipped,
    TotallyFlipped,
}
public class Card : MonoBehaviour
{
    private const string CardFlipSoundVFX = "card_flip";

    [SerializeField] private CardAnimation cardAnimation;
    [SerializeField] private SpriteRenderer backImage;
    [SerializeField] private SpriteRenderer frontImage;
    
    private int _frontCardIndex;
    private bool _mouseDown = false;

    private CardState _cardState = CardState.UnFlipped;

    public bool IsTotallyFlipped => _cardState == CardState.TotallyFlipped;
    public void OnMouseDown()
    {
        _mouseDown = true;
    }

    public void OnMouseUp()
    {
        if (_cardState == CardState.UnFlipped && _mouseDown && CardManager.CanFlip) FlipCard();

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

    private void FlipCard()
    {
        _cardState = CardState.Flipped;
        SoundManager.Instance.PlayAudio(CardFlipSoundVFX);
        CardManager.Instance.FlipCard(this);
        cardAnimation.Flip(true);
    }

    public void UnFlipCard()
    {
        SoundManager.Instance.PlayAudio(CardFlipSoundVFX);
        _cardState = CardState.Flipped;
        cardAnimation.Flip(false);
    }

    public void SendFlippedCardToCardManager()
    {
        _cardState = CardState.TotallyFlipped;
        CardManager.Instance.CheckFlippedCards();
    }

    public void SendUnFlippedCardToCardManager()
    {
        _cardState = CardState.UnFlipped;
    }

    public int GetFaceIndex()
    {
        return _frontCardIndex;
    }
}