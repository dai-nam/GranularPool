using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grain : MonoBehaviour
{
    float grainPosition;
    float grainLength;
    Ball ball;

    public void SetGrainPosition(float position)
    {
        grainPosition = position;
    }

    internal void SetGrainLength(float length)
    {
        grainLength = length;
    }

    internal void SetConnectedBall(Ball ball)
    {
        this.ball = ball;
    }

    public void Update()
    {
        float position = SoundSampler.Instance.ConvertBallPositionToGrainPosition(ball.GetXandZposition());
        float length = SoundSampler.Instance.ConvertBallPositionToGrainLength(ball.GetXandZposition());
        SetGrainPosition(position);
        SetGrainLength(length);
    }
}
