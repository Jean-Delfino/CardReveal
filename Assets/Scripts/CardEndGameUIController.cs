using UI;
using UnityEngine;

public class CardEndGameUIController : MonoBehaviour
{
    [SerializeField] private CardScoreEndGameUI loseGameUI;
    [SerializeField] private CardScoreEndGameUI winGameUI;

    public CardScoreEndGameUI GetGameUI(bool win)
    {
        return win ? winGameUI : loseGameUI;
    }
}