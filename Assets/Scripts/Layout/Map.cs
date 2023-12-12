using System.Collections.Generic;
using UnityEngine;

namespace Layout
{
    [CreateAssetMenu(fileName = "LevelDefinition", menuName = "CardGameDesign/Map")]
    public class Map : ScriptableObject
    {
        [SerializeField] private List<LevelDefinition> levelConfiguration;

        public List<LevelDefinition> LevelConfiguration => levelConfiguration;
    }
}