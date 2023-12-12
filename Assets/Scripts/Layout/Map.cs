using System.Collections.Generic;
using UnityEngine;

namespace Layout
{
    [CreateAssetMenu(fileName = "Map", menuName = "CardGameDesign/Map")]
    public class Map : ScriptableObject
    {
        [SerializeField] private List<LevelDefinition> levelConfiguration;
        [SerializeField] private string mapName;
        
        public List<LevelDefinition> LevelConfiguration => levelConfiguration;
    }
}