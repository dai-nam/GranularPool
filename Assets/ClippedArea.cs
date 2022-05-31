using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippedArea : MonoBehaviour
{

    public void UpdatePosition(Vector3 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
    }

    public void Enable(bool enabled)
    {
        // gameObject.SetActive(b);

        //Statt Objekt an- und ausschalten, dauerhaft an und statt Ausschalten in den Off-Screen verlegen
        if(!enabled)
        {
            Vector3 rectpos = GetComponent<RectTransform>().localPosition;
            float x = rectpos.x;
            float y = rectpos.y;
            float z = rectpos.z;
            GetComponent<RectTransform>().localPosition = new Vector3(1000, y, z);
        }
    }
      

    public void UpdateWidth(float clippingAmount, float maxWidth)
    {
        float clampedWidth = Mathf.Clamp(clippingAmount, 0, maxWidth);
        RectTransform rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clampedWidth);
    }
}
