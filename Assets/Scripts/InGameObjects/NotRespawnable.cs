using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;

namespace Assets.Scripts.InGameObjects
{
    public class NotRespawnable : MonoBehaviour, FallenFromTableBehaviour
    {
        public void HandleFallenFromTable(Ball b)
        {
            BallFactory.Instance.DestroyGameBallAndRemoveFromList(b);
        }
    }
}