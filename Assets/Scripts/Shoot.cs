using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public bool isClicked;
    TargetLine targetLine;
    Ball ball;
    float velocity;

    private void Awake()
    {
        ball = GetComponent<Ball>();
        targetLine = GetComponent<TargetLine>();
    }

    private void Update()
    {
        if (isClicked)
        {
            CalculateVelocity(MouseInput.currentMousePosTranslated);
            DrawTargetLine(ball.transform.position, MouseInput.currentMousePosTranslated);
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }


    private void OnMouseUp()
    {
        if (isClicked)
        {
            ExecuteShot();
        }
        isClicked = false;
    }


    private void ExecuteShot()
    {
        targetLine.DisableTargetLine();
        ball.GetComponent<Rigidbody>().AddForce(targetLine.Direction * 50f);        //hier irgendwie Time.deltaTime rinbringen
        //print("Target: "+targetLine.Direction);
    }

    private void CalculateVelocity(Vector3 mouseVectorTranslated)
    {
        velocity = Vector3.Distance(ball.transform.position, mouseVectorTranslated);
    }

    private void DrawTargetLine(Vector3 start, Vector3 end)
    {
        targetLine.SetThickness(velocity);
        targetLine.SetPoints(start, end);
        targetLine.DrawLine();
    }




   

   
}
