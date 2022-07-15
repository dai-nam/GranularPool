using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingHandler : MonoBehaviour
{

    [SerializeField] GrainRectangle mainArea;
    [SerializeField] ClippedArea clippedAreaLeft, clippedAreaRight;
    RectTransform playheadTransform;
    [HideInInspector] public float clippingAmountLeft, clippingAmountRight;
    private enum ClippingDirection
    {
        LEFT, RIGHT
    }

    Vector3[] backgroundWorldCorners = new Vector3[4];

    private void OnEnable()
    {
        GetComponent<RectTransform>().GetWorldCorners(backgroundWorldCorners);
        playheadTransform = GetComponentInChildren<PlayheadController>().GetComponent<RectTransform>();
    }

    void Update()
    {
        CheckForClippping();
        HandleClipping();
    }

    private void CheckForClippping()
    {
      //  mainArea.clippedLeft = mainArea.clippedRight = false;
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
            mainArea.clippedLeft = mainArea.clippedRight = false;
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
            print("LEFT");

            UpdateWidthWhenClipping(clippingAmountLeft, ClippingDirection.LEFT);
            clippedAreaLeft.UpdatePosition(new Vector3(rt.rect.x + rt.rect.width, 0, 0));
            clippedAreaLeft.UpdateWidth(clippingAmountLeft, mainArea.grainWidthInPixels);
            clippedAreaLeft.Enable(true);
        }
        else if (mainArea.clippedRight)
        {
            print("RIGHT");

            UpdateWidthWhenClipping(clippingAmountRight, ClippingDirection.RIGHT);
            clippedAreaRight.UpdatePosition(new Vector3(rt.rect.x, 0, 0));
            clippedAreaRight.UpdateWidth(clippingAmountRight, mainArea.grainWidthInPixels);
            clippedAreaRight.Enable(true);
        }
        else
        {
            //Kein Clipping
            clippedAreaLeft.Enable(false);
            clippedAreaRight.Enable(false);
        }
    }

    private void UpdateWidthWhenClipping(float clippingAmount, ClippingDirection dir)
    {
        RectTransform rt = mainArea.GetComponent<RectTransform>();

        float newWidth = rt.rect.width - clippingAmount;

        if (dir == ClippingDirection.RIGHT)
        {
            rt.pivot = new Vector2(0, 0.5f);
        }
        else
        {
            //set pivot to right side
            rt.pivot = new Vector2(1f, 0.5f);
        }
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        //Reset pivot
        rt.pivot = new Vector2(0, 0.5f);

       // print(newWidth);

    }

    void ClipPlayhead(ClippingDirection dir)
    {
        if(dir == ClippingDirection.LEFT)
        {
            if(playheadTransform.position.x > mainArea.GetWorldCornersOfRectangle()[0].x)
            {
                playheadTransform.position = mainArea.GetWorldCornersOfSampler()[2];
            }
        }
       else if (dir == ClippingDirection.RIGHT)
        {
            playheadTransform.position = mainArea.GetWorldCornersOfSampler()[0];
        }

    }
}
