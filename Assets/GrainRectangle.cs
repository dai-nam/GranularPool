using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainRectangle : MonoBehaviour
{
    RectTransform rt;
    RectTransform parentRectTransform;

    public GrainData gd;
    Vector3[] worldCornersOfSampler;
    Vector3[] worldCorners;
   [HideInInspector] public bool clippedLeft, clippedRight;
    public float grainWidthInPixels;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        gd = transform.parent.GetComponent<GrainData>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        worldCornersOfSampler = new Vector3[4];
        parentRectTransform.GetWorldCorners(worldCornersOfSampler);
        worldCorners = new Vector3[4];
       // grainWidth = GetComponent<RectTransform>().rect.width;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateWidth();
    }

    private void UpdateWidth()
    {
         grainWidthInPixels = gd.grainWidth * parentRectTransform.rect.width;
        //grainWidthInPixels = gd.grainWidth * UiAudioManager.Instance.GetComponent<RectTransform>().rect.width;

        if (!clippedLeft && !clippedRight)
        {
         //   print("GUer");
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, grainWidthInPixels);

        }
    }

    private void UpdatePosition()
    {
        rt.position = parentRectTransform.position + (Vector3.right * gd.grainPosition * parentRectTransform.rect.width);
    }



    public Vector3[] GetCornersWithRelativePosition()
    {
        Vector3[] cornersWithRelativePosition = new Vector3[4];
        rt.GetWorldCorners(worldCorners);
        Vector3 v = new Vector3();
        for (int i = 0; i < 4; i++)
        {
            Vector3 tmp = worldCorners[i] - worldCornersOfSampler[i%2];      // %2 weil relative Position aller Ecken zu den linken Ecken (0 und 1) des Hintergrunds bestimmt werden soll 
            float width = parentRectTransform.rect.width;
            v.Set(tmp.x / width, tmp.y, tmp.z);
            //print(i + ": " + v);
            cornersWithRelativePosition[i] = v;
        }

        return cornersWithRelativePosition;
    }

    public Vector3[] GetWorldCornersOfRectangle()
    {
        return worldCorners;
    }

    public Vector3[] GetWorldCornersOfSampler()
    {
        return worldCornersOfSampler;
    }

}
