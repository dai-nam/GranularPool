using UnityEngine;
using System.Collections;

namespace Assets.Scripts.InGameObjects
{
    public abstract class FallenFromTable : MonoBehaviour
    {

        public abstract void HandleFallenFromTable(Ball b);
    }
}