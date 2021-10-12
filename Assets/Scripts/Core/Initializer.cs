using System.Collections;
using UnityEngine;
using Assets.Scripts.InGameObjects;


namespace Assets.Scripts.Core
{
    public class Initializer : MonoBehaviour
    {
        public delegate void CreateNewBalls();
        public static event CreateNewBalls OnCreateNewBalls;

        public void InstantiateNewBallsAndGrains()
        {
            OnCreateNewBalls?.Invoke();
        }
    }
}