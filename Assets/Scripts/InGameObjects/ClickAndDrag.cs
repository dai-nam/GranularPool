using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;


namespace Assets.Scripts.InGameObjects
{
    public class ClickAndDrag : MonoBehaviour
    {
        bool isClicked;

        private void Update()
        {
            if (isClicked)
            {
                Move();
            }
        }


        private void OnMouseDown()
        {
            isClicked = true;
        }


        private void OnMouseUp()
        {
            isClicked = false;
        }

        void Move()
        {
            transform.position = MouseInput.currentMousePosTranslated;
        }
    }
}