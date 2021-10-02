using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InGameObjects;

namespace Assets.Scripts.Core
{
    public class BallFactory : MonoBehaviour
    {
        [SerializeField] CueBall cueBall;
        [SerializeField] GameBall gameBall;
        [SerializeField] public TestBall testBall1;
        [SerializeField] public TestBall testBall2;


        public List<Ball> gameBalls = new List<Ball>();
        [SerializeField] [Range(0, 100)] List<int> probabilites;
        [SerializeField] [Range(0f, 1f)] float bounciness;
        public static BallFactory Instance;

        public delegate void BallsCreated();
        public static event BallsCreated OnBallsCreated;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AddProbabilitesForBallIds();
            MakeGameBalls(GameManager.Instance.ballCount);
            OnBallsCreated?.Invoke();

            MakeCueBall();

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
                b.level = SelectGameBallId(ranges);
                gameBalls.Add(b);
                // b.OnHitRespawnPlane += DestroyBallAndRemoveFromList;
                b.OnHitRespawnPlane += b.GetComponent<FallenFromTable>().HandleFallenFromTable;
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

        public void DestroyBallAndRemoveFromList(Ball b)
        {
            gameBalls.Remove(b);
          //  b.OnHitRespawnPlane -= DestroyBallAndRemoveFromList;
            b.OnHitRespawnPlane -= b.GetComponent<FallenFromTable>().HandleFallenFromTable;
            Destroy(b.gameObject);
        }


        private void DestroyAllBalls()
        {
            foreach (Ball b in transform.GetComponentsInChildren<Ball>())
                DestroyBallAndRemoveFromList(b);
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
}