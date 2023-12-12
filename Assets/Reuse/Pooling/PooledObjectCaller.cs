using System.Collections.Generic;
using Reuse.Pooling;
using UnityEngine;

namespace Reuse.Pooling
{
    public class PooledObjectCaller : MonoBehaviour
    {
        public List<Spawner.PooledObjects> toSpawn = new();
        
        public void SpawnPoolItems()
        {
            Spawner.SendToPool(toSpawn);
        }
    }
}