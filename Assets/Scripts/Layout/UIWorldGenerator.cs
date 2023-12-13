using System;
using Layout.UI;
using UnityEngine;

namespace Layout
{
    public class UIWorldGenerator : WorldGenerator
    {
        [SerializeField] private UIWorldLayout layout;

        public void Start()
        {
            GenerateWorld();
        }

        public override void GenerateWorld()
        {
            layout.SpawnWorld(world);  
        }
    }
}