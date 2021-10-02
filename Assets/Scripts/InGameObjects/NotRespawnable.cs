using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;

namespace Assets.Scripts.InGameObjects
{
    public class NotRespawnable : FallenFromTable
    {
        public override void HandleFallenFromTable(Ball b)
        {
            BallFactory.Instance.DestroyBallAndRemoveFromList(b);
        }
    }
}