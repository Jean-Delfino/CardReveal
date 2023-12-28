using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//https://github.com/Maraakis/ChristinaCreatesGames/blob/main/Open%20URL%20in%20Browser%20when%20clicked%20on%20Link/LinkHandlerForTMPText.cs
//Maraakis
//https://www.youtube.com/watch?v=lEpigNZwM-4&ab_channel=ChristinaCreatesGames

namespace Reuse.TextMeshPro
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkHandlerForTMPTextWithURLClick : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _tmpTextBox;
        private Canvas _canvasToCheck;
        private Camera _cameraToUse;
        
        public static event Action<string> OnClickedOnLink;

        private void Awake()
        {
            _tmpTextBox = GetComponent<TMP_Text>();
            _canvasToCheck = GetComponentInParent<Canvas>();

            if (_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
                _cameraToUse = null;
            else
                _cameraToUse = _canvasToCheck.worldCamera;
        }
        

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);

            var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePosition, _cameraToUse);

            if (linkTaggedText == -1) return;
            
            TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[linkTaggedText];
            
            string linkID = linkInfo.GetLinkID();
            if (linkID.StartsWith("http://") || linkID.StartsWith("https://"))
            {
                Application.OpenURL(linkID);
                return;
            }

            OnClickedOnLink?.Invoke(linkInfo.GetLinkText());
        }
    }
}