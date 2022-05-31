using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainRectangle : MonoBehaviour
{
    RectTransform parentRectTransform;
    Vector3[] worldCornersOfBackground;
    Vector3[] worldCorners;
    public bool clippedLeft, clippedRight;
    public float grainWidth;

    private void Start()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        worldCornersOfBackground = new Vector3[4];
        parentRectTransform.GetWorldCorners(worldCornersOfBackground);
        worldCorners = new Vector3[4];
        grainWidth = GetComponent<RectTransform>().rect.width;
        print(grainWidth);
    }

    public void UpdateWidth()
    {

    }

    public Vector3[] GetCornersWithRelativePosition()
    {
        Vector3[] cornersWithRelativePosition = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        Vector3 v = new Vector3();
        for (int i = 0; i < 4; i++)
        {
            Vector3 tmp = worldCorners[i] - worldCornersOfBackground[i%2];      // %2 weil relative Position aller Ecken zu den linken Ecken (0 und 1) des Hintergrunds bestimmt werden soll 
            float width = parentRectTransform.rect.width;
            v.Set(tmp.x / width, tmp.y, tmp.z);
            //print(i + ": " + v);
            cornersWithRelativePosition[i] = v;
        }

        return cornersWithRelativePosition;
    }
}
