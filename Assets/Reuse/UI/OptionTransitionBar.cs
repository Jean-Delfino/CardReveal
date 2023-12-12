using UnityEngine;

namespace Reuse.UI
{
    public class OptionTransitionBar : MonoBehaviour
    {
        [SerializeField] private Vector3[] barsPosition;
        [SerializeField] private Transform movableObject;

        private int _actualBar;
        public void SetBar(int value)
        {
            _actualBar = value;
            MoveMovableSetter();
        }

        private void MoveMovableSetter()
        {
            movableObject.localPosition = barsPosition[_actualBar];
        }
    }
}