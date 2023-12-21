using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardScoreEndGameUI : MonoBehaviour
    {
        [SerializeField] private Image starImage;
        
        [Serializable]
        public class ScoreTypeText
        {
            public ScoreType type;
            public TextMeshProUGUI text;
            public string prefix = "X";
            public string suffix;
        }

        [SerializeField] private ScoreTypeText[] texts;
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private Button nextMapButton;

        private void OnEnable()
        {
            if(nextMapButton == null) return;

            nextMapButton.enabled = CardManager.Instance.HasNextLevel();
        }

        public void Setup(int star, float score, Dictionary<ScoreType, int> reveals)
        {
            starImage.sprite = VisualProvider.Instance.GetStarSprite(star);
            scoreText.text = $"{score}";

            var amount = 0f;
            
            foreach (var scoreGroup in texts)
            {
                amount = reveals.ContainsKey(scoreGroup.type) ? reveals[scoreGroup.type] : 0;

                scoreGroup.text.text = $"{scoreGroup.prefix}{amount}{scoreGroup.suffix}";
            }
            
            gameObject.SetActive(true);
        }
    } 
}