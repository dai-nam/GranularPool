using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InGameObjects;

namespace Assets.Scripts.Core
{

    public class BallFactory : MonoBehaviour
    {
        [SerializeField]  CueBall cueBall;
        [SerializeField] GameBall gameBall;

        public List<Ball> gameBalls;
        [SerializeField] [Range(0, 100)] List<int> probabilites;
        [SerializeField] [Range(0f, 1f)] float bounciness;
        public static BallFactory Instance;

        public delegate void NewBallsCreated();
        public static event NewBallsCreated OnNewBallsCreated;
        public delegate void BallDestroyed(Ball b);
        public static event BallDestroyed OnBallDestroyed;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AddDefaultProbabilitesForEachBallLevel();
            Initializer.OnCreateNewBalls += MakeCueBall;
            Initializer.OnCreateNewBalls += MakeGameBalls;
        }

        public void MakeCueBall()
        {
            if (CheckIfCueBallAlreadyExists())
            {
                return;
            }

            CueBall b = Instantiate(cueBall, Table.Instance.GetRandomPositionOnTable(), Quaternion.identity);
            b.OnHitRespawnPlane += b.GetComponent<FallenFromTableBehaviour>().HandleFallenFromTable;
            b.transform.parent = this.transform;
        }

        private static bool CheckIfCueBallAlreadyExists()
        {
            return BallFactory.Instance.transform.GetComponentInChildren<CueBall>() != null;
        }

        public void MakeGameBalls()
        {
            DestroyAllGameBalls();
            List<int> ranges = new List<int>();
            CalculateLevelProbabilityRanges(ranges);
            int limit = GameManager.Instance.ballCount;

            for (int i = 0; i < limit; i++)
            {
                GameBall b = Instantiate(gameBall, Table.Instance.GetRandomPositionOnTable(), Quaternion.identity);
                b.transform.parent = this.transform;
                b.level = PickLevelStringBasedOnProbabilities(ranges);
                gameBalls.Add(b);
                b.OnHitRespawnPlane += b.GetComponent<FallenFromTableBehaviour>().HandleFallenFromTable;
            }
            OnNewBallsCreated?.Invoke();
        }

        private void AddDefaultProbabilitesForEachBallLevel()
        {
            int total = System.Enum.GetValues(typeof(BallLevel)).Length-1;
            for (int i = 0; i < total; i++)
            {
                probabilites.Add(100);
            }
        }

        private void CalculateLevelProbabilityRanges(List<int> ranges)
        {
            int runningSum = 0; ;
            ranges.Add(0);
            foreach (int i in probabilites)
            {
                runningSum += i;
                ranges.Add(runningSum);
            }
        }


        private BallLevel PickLevelStringBasedOnProbabilities(List<int> ranges)
        {
            int total = 0;
            foreach (int i in probabilites)
            {
                total += i;
            }
            int num = Random.Range(0, total);

            for (int i = 0; i < ranges.Count-1; i++)
            {
                if (num >= ranges[i] && num < ranges[i+1])
                    return (BallLevel)System.Enum.GetValues(typeof(BallLevel)).GetValue(i);

            }
            return BallLevel.LEVEL_UNDEFINED;
        }

        public void DestroyGameBallAndRemoveFromList(Ball gameBall)
        {
            gameBalls.Remove(gameBall);
            gameBall.OnHitRespawnPlane -= gameBall.GetComponent<FallenFromTableBehaviour>().HandleFallenFromTable;
            OnBallDestroyed?.Invoke(gameBall);
            Destroy(gameBall.gameObject);
        }


        public void DestroyAllGameBalls()
        {
            gameBalls = new List<Ball>();
            foreach (GameBall gb in transform.GetComponentsInChildren<GameBall>())  // -> unschön, dass hier konkretes Game´ball
                DestroyGameBallAndRemoveFromList(gb);
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