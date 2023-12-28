using Reuse.Sound;
using SaveGame;
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

        [SerializeField] private string clickButtonAudio;
        
        private LevelDefinition _level;
        private Map _map;
        public void OnEnable()
        {
            starImage.sprite = VisualProvider.Instance.GetStarSprite(UtilCardSave.GetStarAmount(_level.GetSaveName()));
        }

        private void OnClick()
        {
            CardManager.Instance.SetLevel(_map, _level);
            CardGameTransitionController.Instance.MakeTransition(CardGameTransitionController.GameState
                .MapSelectionToGame);
            
            SoundManager.Instance.PlayAudio(clickButtonAudio);
        }

        public void Setup(Map map, LevelDefinition level, int levelIndex)
        {
            _map = map;
            _level = level;
            button.onClick.AddListener(OnClick);

            levelName.text = $"{levelIndex}";
        }
    }
}