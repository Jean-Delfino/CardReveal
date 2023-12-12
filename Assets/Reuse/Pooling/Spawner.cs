using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Pooling
{
    public class Spawner : MonoBehaviour
    {
        public List<PooledObjects> initialPooledObjects;

        private static Spawner _instance;

        private readonly Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();
        
        [Serializable]
        public class PooledObjects
        {
            public int amount;
            public GameObject gameObject;
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            SendToPool(_instance.initialPooledObjects);
        }
        
        public static void SendToPool(List<PooledObjects> toPool)
        { 
            foreach (var pooled in toPool)
            {
                var pool = GetPool(pooled.gameObject);
                for (int i = 0; i < pooled.amount; i++)
                {
                    var go = Instantiate(pooled.gameObject, _instance.transform);
                    go.SetActive(false);
                    
                    pool.Enqueue(go);
                }
            }
        }

        public static GameObject Spawn(GameObject gameObject)
        {
            var pooled = GetPool(gameObject);
            GameObject res;
            
            if (pooled.Count == 0)
            {
                res = Instantiate(gameObject);
            }
            else
            {
                res = pooled.Dequeue();
                res.SetActive(true);
            }
            
            res.name = gameObject.name;
            return res;
        }

        public static GameObject Spawn(GameObject gameObject, Transform parent)
        {
            var go = Spawn(gameObject);
            go.transform.SetParent(parent);

            return go;
        }

        public static GameObject Spawn(GameObject gameObject, Vector3 position)
        {
            var go = Spawn(gameObject);
            go.transform.position = position;

            return go;
        }
        
        public static GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            var go = Spawn(gameObject, position);
            go.transform.rotation = rotation;

            return go;
        }

        public static GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
        {
            var go = Spawn(gameObject, position, rotation);
            go.transform.SetParent(parent);

            return go;
        }

        public static void Despawn(GameObject gameObject)
        {
            var pooled = GetPool(gameObject);
            
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_instance.transform);
            
            pooled.Enqueue(gameObject);
        }
        
        public static void Despawn(GameObject gameObject, float duration)
        { 
            _instance.StartCoroutine(WaitAndDespawn(gameObject, duration));
        }

        private static IEnumerator WaitAndDespawn(GameObject gameObject, float duration)
        {
            yield return new WaitForSeconds(duration);
            Despawn(gameObject);
        }

        private static Queue<GameObject> GetPool(GameObject gameObject)
        {
            var key = gameObject.name;

            if (!_instance._pool.ContainsKey(key)) _instance._pool.Add(key, new Queue<GameObject>());

            return _instance._pool[key];
        }
    }
}