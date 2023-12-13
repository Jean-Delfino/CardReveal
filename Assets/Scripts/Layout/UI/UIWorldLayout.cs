using UnityEngine;

namespace Layout.UI
{
    public class UIWorldLayout : MonoBehaviour
    {
        [SerializeField] private UILevelLayout levelLayout;
        [SerializeField] private UIMapLayout mapLayout;
        [SerializeField] private Transform spawnPosition;

        public void SpawnWorld(World world)
        {
            foreach (var map in world.MapsConfiguration)
            {
                var mapClone = Instantiate(mapLayout, this.spawnPosition);
                
                mapClone.SetMap(map, levelLayout);
            }
        }
    }
}