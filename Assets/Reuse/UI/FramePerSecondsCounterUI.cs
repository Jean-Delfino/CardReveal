using System.Collections;
using TMPro;
using UnityEngine;

namespace Reuse.UI
{
    public class FramePerSecondsCounterUI : MonoBehaviour
    {
        [SerializeField] private float deltaTime;

        private TextMeshProUGUI _fpsText;
        
        private void Start()
        {
            _fpsText = GetComponent<TextMeshProUGUI>();
            StartCoroutine(UpdateFpsText());
        }

        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        private IEnumerator UpdateFpsText()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                float fps = 1.0f / deltaTime;
                _fpsText.text = $"{Mathf.Ceil(fps)}";
            }
        }

    }
}