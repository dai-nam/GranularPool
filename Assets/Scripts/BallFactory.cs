using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] CueBall cueBall;
    [SerializeField] GameBall gameBall;
    public List<Ball> gameBalls = new List<Ball>();
    [SerializeField] [Range(0, 100)] List<int> probabilites;
    [SerializeField] [Range(0f, 1f)] float bounciness;


    private void Start()
    {
        MakeCueBall();

        AddProbabilitesForBallIds();
        MakeGameBalls(GameManager.Instance.ballCount);
    }


    private void MakeCueBall()
    {
        CueBall b = Instantiate(cueBall, Table.Instance.GetRandomPositionOnTable(), Quaternion.identity);
        b.transform.parent = this.transform;
    }



    private void MakeGameBalls(int num)
    {
        List<int> ranges = new List<int>();
        CalculateRanges(ranges);

        for (int i = 0; i < num; i++)
        {
            GameBall b = Instantiate(gameBall, Table.Instance.GetRandomPositionOnTable(), Quaternion.identity);
            b.transform.parent = this.transform;
            b.id = SelectGameBallId(ranges);
            gameBalls.Add(b);
            b.OnFallenFromTable += HandleFallFromTable;
        }
    }

    private void AddProbabilitesForBallIds()
    {
        int total = GameManager.Instance.numberOfSamples;
        for (int i = 0; i < total; i++)
        {
            probabilites.Add(100 / total);
        }
    }

    private int SelectGameBallId(List<int> ranges)
    {
        int total = 0;
        foreach (int i in probabilites)
        {
            total += i;
        }
        int num = Random.Range(0, total);

        for (int i = 1; i < ranges.Count; i++)
        {
            if (num >= ranges[i - 1] && num < ranges[i])
                return i - 1;
        }
        return -1;
    }

    private void CalculateRanges(List<int> ranges)
    {
        int runningSum = 0; ;
        ranges.Add(0);
        foreach (int i in probabilites)
        {
            runningSum += i;
            ranges.Add(runningSum);
        }
    }

    private void HandleFallFromTable(Ball b)
    {
        RemoveGameBallFromList(b);
        b.OnFallenFromTable -= HandleFallFromTable;
        DestroyBall(b);
    }

    public void RemoveGameBallFromList(Ball b)                    //unelegant
    {
      //  Ball x = gameBalls.Find(ball => b.instanceId == ball.instanceId);
        gameBalls.Remove(b);
    }

    public void DestroyBall(Ball b)
    {
        Destroy(b.gameObject);
    }

    private void DestroyAllBalls()
    {
        foreach (Ball b in transform.GetComponentsInChildren<Ball>())
            DestroyBall(b);
    }


    private void OnValidate()
    {
        foreach (GameBall gb in gameBalls)
        {
            if (gb == null)
                continue;
            gb.SetBallBounce(bounciness);          // todo: eleganter, kein Null Check
        }

    }
}
