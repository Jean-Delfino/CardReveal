using System;
using System.Linq;
using Reuse.CSV;
using Reuse.Utils;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardScoreControllerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField] private Animator scoreTypeAnimator;
        [SerializeField] private VersatileText scoreTypeText;
        
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
            scoreTypeText.SetKey(score.text);
            UtilTextMeshPro.SetGradient(scoreTypeText.GetTextMeshProUGUI(), score.gradient);
            
            scoreTypeAnimator.SetTrigger("Score");
        }
    }
}