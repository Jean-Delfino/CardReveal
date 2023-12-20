using System;
using System.Linq;
using Reuse.Utils;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardScoreControllerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField] private Animator scoreTypeAnimator;
        [SerializeField] private TextMeshProUGUI scoreTypeText;
        
        [Serializable]
        public class ScoreTypeWithText
        {
            public ScoreType type;
            public string text;
            public UtilTextMeshPro.SidedGradient gradient;
        }

        [SerializeField] private ScoreTypeWithText[] scoreTextsDefinition;

        public void ResetScore(int defaultValue)
        {
            ShowTextScore(defaultValue);
        }
        public void ShowTextScore(float value)
        {
            scoreText.text = $"{value}";
        }

        public void ShowScoreType(ScoreType scoreType)
        {
            var score = scoreTextsDefinition.First(e => e.type == scoreType);
            scoreTypeText.text = score.text;
            UtilTextMeshPro.SetGradient(scoreTypeText, score.gradient);
            
            scoreTypeAnimator.SetTrigger("Score");
        }
    }
}