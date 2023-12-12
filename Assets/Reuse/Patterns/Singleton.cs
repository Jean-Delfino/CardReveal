using UnityEngine;

namespace Reuse.Patterns
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        [SerializeField] private bool destroyOnLoad = true;

        protected virtual void Awake()
        {
            if (_instance == null) _instance = this.GetComponent<T>();
            else Destroy(this.gameObject);
            
            if(!destroyOnLoad) DontDestroyOnLoad(_instance);
        }
    }
}
