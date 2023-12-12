using UnityEngine;

namespace Layout
{
    public class LevelSelectButton : MonoBehaviour
    {
        private LevelDefinition _level;

        private void OnClick()
        {
            
        }

        private void Setup(LevelDefinition level)
        {
            _level = level;
        }
    }
}