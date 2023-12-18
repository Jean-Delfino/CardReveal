using TMPro;
using UnityEngine;

namespace Layout.UI
{
    public class UIMapLayout : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mapName;
        [SerializeField] private Transform levelSpawn;
        public void SetMap(Map map, UILevelLayout levelLayout)
        {
            int levelCount = 1;
            
            foreach (var level in map.LevelConfiguration)
            {
                var levelClone = Instantiate(levelLayout, levelSpawn);
                levelClone.Setup(level, levelCount);
                
                levelClone.gameObject.SetActive(true);
                levelCount++;
            }
            
            mapName.text = map.MapName;
        }
    }
}