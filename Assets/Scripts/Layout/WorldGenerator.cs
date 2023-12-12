using UnityEngine;

namespace Layout
{
    public abstract class WorldGenerator : MonoBehaviour
    {
        [SerializeField] protected World world;

        public abstract void GenerateWorld();
    } 
}