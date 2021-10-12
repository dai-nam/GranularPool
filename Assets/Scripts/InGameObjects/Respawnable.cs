using UnityEngine;
using System.Collections;

namespace Assets.Scripts.InGameObjects
{
    public class Respawnable : MonoBehaviour, FallenFromTableBehaviour 
    {

        public void RespawnAtRandomPosition()
        {
            Vector3 randomPosition = Table.Instance.GetRandomPositionOnTable();
            transform.position = randomPosition;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }


        public void HandleFallenFromTable(Ball b)
        {
                RespawnAtRandomPosition();
        }
    }
}