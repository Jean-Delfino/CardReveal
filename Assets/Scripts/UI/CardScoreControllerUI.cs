using TMPro;
using UnityEngine;

namespace UI
{
    public class CardScoreControllerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void ShowTextScore(float value)
        {
            scoreText.text = $"{value}";
        }
    }
}