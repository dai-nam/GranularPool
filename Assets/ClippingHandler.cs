using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingHandler : MonoBehaviour
{

    [SerializeField] GrainRectangle mainArea;
    [SerializeField] ClippedArea clippedAreaLeft, clippedAreaRight;
    [HideInInspector] public float clippingAmountLeft, clippingAmountRight;
    private enum ClippingDirection
    {
        LEFT, RIGHT
    }

    Vector3[] backgroundWorldCorners = new Vector3[4];

    private void Start()
    {
        GetComponent<RectTransform>().GetWorldCorners(backgroundWorldCorners);
    }

    void Update()
    {
        CheckForClippping();
        HandleClipping();
    }

    private void CheckForClippping()
    {
        mainArea.clippedLeft = mainArea.clippedRight = false;
        Vector3[] cornersWithRelativePosition = mainArea.GetCornersWithRelativePosition();
        //Linke Ecke Clipped Left
        if (cornersWithRelativePosition[0].x < 0f)
        {
            mainArea.clippedLeft = true;
            clippingAmountLeft = GetClippingAmount(ClippingDirection.LEFT);
        }
            //Rechte Ecke Clipped Right
       else if (cornersWithRelativePosition[2].x > 1f)
        {
            mainArea.clippedRight = true;
            clippingAmountRight = GetClippingAmount(ClippingDirection.RIGHT);
        }
        else
        {
            clippingAmountLeft = clippingAmountRight = 0;
        }


        // print("ClippedLeft=" + clippedLeft + "   ClippedRight=" + clippedRight);
    }

    private float GetClippingAmount(ClippingDirection dir)
    {
        int index;
        if (dir == ClippingDirection.LEFT)
            index = 0;
        else if (dir == ClippingDirection.RIGHT)
            index = 2;
        else
            return -1f;

        Vector3[] mainAreaWorldCorners = new Vector3[4];
        mainArea.GetComponent<RectTransform>().GetWorldCorners(mainAreaWorldCorners);
        float f = Mathf.Abs(mainAreaWorldCorners[index].x - backgroundWorldCorners[index].x);
        return f;
    }


    private void HandleClipping()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (mainArea.clippedLeft)
        {
            mainArea.UpdateWidth();
            clippedAreaLeft.UpdatePosition(new Vector3(rt.rect.x + rt.rect.width, 0, 0));
            clippedAreaLeft.UpdateWidth(clippingAmountLeft, mainArea.grainWidth);
            clippedAreaLeft.Enable(true);
        }
        else if (mainArea.clippedRight)
        {
            mainArea.UpdateWidth();
            clippedAreaRight.UpdatePosition(new Vector3(rt.rect.x, 0, 0));
            clippedAreaRight.UpdateWidth(clippingAmountRight, mainArea.grainWidth);
            clippedAreaRight.Enable(true);
        }
        else
        {
            //Kein Clipping
            clippedAreaLeft.Enable(false);
            clippedAreaRight.Enable(false);
        }
    }
}
