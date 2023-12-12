using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just changed the Rotate and small things
//Everything else is this person code
//Алиса Хомич https://github.com/Alice3529/BookTutorial
namespace Reuse.UI
{
    public class BookMenu : MonoBehaviour
    {
        [SerializeField] private float pageSpeed = 0.5f;
        [SerializeField] private List<Transform> pages;
        [SerializeField] private GameObject backButton;
        [SerializeField] private GameObject forwardButton;

        private int index = -1;
        private bool isRotating = false;


        private void Start()
        {
            InitialState();
        }

        public void InitialState()
        {
            for (int i=0; i<pages.Count; i++)
            {
                pages[i].transform.rotation=Quaternion.identity;
            }
            pages[0].SetAsLastSibling();
            backButton.SetActive(false);

        }

        public void RotateForward()
        {
            if (isRotating == true) { return; }
            index++;
            float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
            ForwardButtonActions();
            pages[index].SetAsLastSibling();
            pages[index].gameObject.SetActive(true);
            StartCoroutine(RotatePage(angle, true));
        }

        public void ForwardButtonActions()
        {
            if (backButton != null && backButton.activeInHierarchy == false)
            {
                backButton.SetActive(true); //every time we turn the page forward, the back button should be activated
            }
            if (forwardButton != null && index == pages.Count - 1)
            {
                forwardButton.SetActive(false); //if the page is last then we turn off the forward button
            }
        }

        public void RotateBack()
        {
            if (isRotating == true) { return; }
            float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
            pages[index].SetAsLastSibling();
            pages[index].gameObject.SetActive(true);
            BackButtonActions();
            StartCoroutine(RotatePage(angle, false));
        }

        public void BackButtonActions()
        {
            if (forwardButton != null && forwardButton.activeInHierarchy == false)
            {
                forwardButton.SetActive(true); //every time we turn the page back, the forward button should be activated
            }
            if (backButton != null && index - 1 == -1)
            {
                backButton.SetActive(false); //if the page is first then we turn off the back button
            }
        }
        
        private IEnumerator RotatePage(float angle, bool forward)
        {
            if (isRotating)
            {
                yield break; // Retorna se a rotação já estiver em andamento.
            }

            isRotating = true;

            float value = 0f;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            Quaternion initialRotation = pages[index].rotation;

            while (value < 1f)
            {
                value += Time.deltaTime * pageSpeed;
                pages[index].rotation = Quaternion.Slerp(initialRotation, targetRotation, value); // Gira a página suavemente
                yield return null;
            }

            pages[index].rotation = targetRotation;

            if (!forward)
            {
                index--;
            }

            isRotating = false;
        }
    }
}

