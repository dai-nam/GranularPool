using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.InGameObjects;



public class SoundSampler : MonoBehaviour
{
    public static SoundSampler Instance;
    [SerializeField] Grain grain1;
    [SerializeField] Grain grain2;

    List<Grain> grains;
    [SerializeField] public float maxGrainLength = 2000;


    private void Awake()
    {
        grains = new List<Grain>();
        Instance = this;
        BallFactory.OnBallsCreated += InitGrains;
    }


    private void InitGrains()
    {
        /*
        foreach (Ball ball in BallFactory.Instance.gameBalls)
        {
            Grain g = Instantiate(grain);
            g.SetGrainPosition(ConvertBallPositionToGrainPosition(ball.GetXandZposition()));
            g.SetGrainLength(ConvertBallPositionToGrainLength(ball.GetXandZposition()));
            g.transform.parent = this.transform;
        }
        */
        grain1.SetConnectedBall(BallFactory.Instance.testBall1);
        grain2.SetConnectedBall(BallFactory.Instance.testBall2);

        grain1.SetGrainId(BallFactory.Instance.testBall1.testBallId);
        grain2.SetGrainId(BallFactory.Instance.testBall2.testBallId);
        grains.Add(grain1);
        grains.Add(grain2);
    }

    public float ConvertBallPositionToGrainPosition(Vector2 ballPosition)
    {
        if (ballPosition.x == 0)
            return 0;

        float angle = Mathf.Atan2(ballPosition.y, ballPosition.x) * (180 / Mathf.PI);
        if (angle < 0.0)
        {
            angle += 360f;
        }

        float temp = Mathf.InverseLerp(0f, 360f, angle);
        float samplePosition = Mathf.Lerp(0f, 1f, temp);
        samplePosition = Mathf.Abs(samplePosition - 1f);
        return samplePosition;
    }

    public float ConvertBallPositionToGrainLength(Vector2 ballPosition)
    {
        float dist = Vector2.Distance(Table.Instance.GetCenterXandZposition(), ballPosition);
        float temp = Mathf.InverseLerp(0f, Table.Instance.radius, dist);
        float grainLength = Mathf.Lerp(0f, maxGrainLength, temp);
        return grainLength;
    }

    public List<Grain> GetGrains()
    {
        return grains;
    }

}
