using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlane : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Ball b = other.GetComponent<Ball>();
        if (b.isRespawnable)
        {
            b.Respawn();
        }
        else
        {
            b.Destroy();
        }

    }
}
