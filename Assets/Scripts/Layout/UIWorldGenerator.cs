using System;
using System.Collections;
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
            StartCoroutine(WaitAndSet());
        }

        private IEnumerator WaitAndSet()
        {
            yield return new WaitUntil(() => CardManager.Instance != null);
            CardManager.Instance.SetWorld(world);
            layout.SpawnWorld(world);  
        }
    }
}