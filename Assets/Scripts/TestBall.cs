using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : Ball
{
    static int _testBallId;
    bool isClicked;
    public int testBallId;

    void Awake()
    {
        testBallId = _testBallId++;
    }

    private void Update()
    {
       float angle = SoundSampler.Instance.ConvertBallPositionToGrainPosition(GetXandZposition());
        if (isClicked)
        {
            ClickAndDrag();
        }
       // print("XZ: " + Table.Instance.GetCenterXandZposition()+" Angle: "+angle);
    }


    private void OnMouseDown()
    {
        isClicked = true;
    }


    private void OnMouseUp()
    {
        isClicked = false;
    }

    void ClickAndDrag()
    {
        transform.position = MouseInput.currentMousePosTranslated;
    }
}
