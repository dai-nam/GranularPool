using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class MouseInput : MonoBehaviour
    {
        public static Vector3 currentMousePosTranslated;


        void Awake()
        {
            currentMousePosTranslated = new Vector3();
        }


        void Update()
        {
            TranslateMousePositionToWorldPosition();
        }

        private void TranslateMousePositionToWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                currentMousePosTranslated = raycastHit.point;
                currentMousePosTranslated.y = 2.5f;
            }
        }
    }
}
