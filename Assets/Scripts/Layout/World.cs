using System.Collections.Generic;
using UnityEngine;

namespace Layout
{
    [CreateAssetMenu(fileName = "World", menuName = "CardGameDesign/World")]
    public class World : ScriptableObject
    {
        [SerializeField] private List<Map> mapsConfiguration;
        
        public List<Map> MapsConfiguration => mapsConfiguration;
    }
}