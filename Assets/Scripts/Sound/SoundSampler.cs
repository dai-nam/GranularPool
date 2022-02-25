using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;
using Assets.Scripts.InGameObjects;


public class SoundSampler : MonoBehaviour
{
    public static SoundSampler Instance;
    [SerializeField] Grain grain;

    public List<Grain> grains;
    [SerializeField] public float maxGrainLength = 2000;


    private void Awake()
    {
        grains = new List<Grain>();
        Instance = this;
    }

    private void Start()
    {
        BallFactory.OnNewBallsCreated += MakeGrains;
        BallFactory.OnBallDestroyed += DestroyGrainAndRemoveFromList;
    }



    public void MakeGrains()
    {
        DestroyAllGrains();

        foreach (GameBall ball in BallFactory.Instance.gameBalls)
        {
            Grain g = Instantiate(grain);
            g.SetGrainPosition(ConvertBallPositionToGrainPosition(ball.GetXandZposition()));
            g.SetGrainLength(ConvertBallPositionToGrainLength(ball.GetXandZposition()));
            g.SetConnectedBall(ball);
            g.SetGrainId(ball.instanceId);
            g.SetGrainSampleId((int) ball.level);

            g.transform.parent = this.transform;
            grains.Add(g);
        }
    }

    private void DestroyGrainAndRemoveFromList(Ball b)
    {
        Grain g = grains.Find(grain => grain.connectedBall == b);
        grains.Remove(g);
        Destroy(g.gameObject);
    }

    private void DestroyAllGrains()
    {
        foreach (Grain grain in grains)
        {
            Destroy(grain.gameObject);
        }
        grains = new List<Grain>();
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
