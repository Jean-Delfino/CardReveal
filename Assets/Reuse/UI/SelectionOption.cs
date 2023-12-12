using UnityEngine;
using UnityEngine.UI;

namespace Reuse.UI
{
    public class SelectionOption : MonoBehaviour
    {
        [SerializeField] private Transform arrowPosition;
        [SerializeField] private int line;
        [SerializeField] private int column;
        [SerializeField] private GameObject onClick;

        private Button _button;
        
        public int Line => line;
        public int Column => column;
        public Transform ArrowPosition => arrowPosition;

        private bool _locked = false;

        public bool Locked => _locked;

        void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        public virtual void Execute(){
            if(onClick) onClick.SetActive(true);
        }

        public void Select(){
            _button.Select();
        }

        public void MouseLock(){
            _locked = true;
        }

        public void MouseUnlock(){
            _locked = false;
        }
    }
}