using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Layout.UI
{
    public class UILevelLayout : MonoBehaviour
    {
        [Space] [Header("DYNAMIC VISUAL")] [Space]
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private Image starImage;
        
        [Space] [Header("LEVEL SELECTION BUTTON")] [Space]
        
        [SerializeField] private Button button;
        private LevelDefinition _level;

        private void OnClick()
        {
            CardManager.Instance.SetLevel(_level);
        }

        public void Setup(LevelDefinition level, int levelIndex)
        {
            _level = level;
            button.onClick.AddListener(OnClick);

            levelName.text = $"{levelIndex}";
        }
    }
}