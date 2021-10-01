using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationController : MonoBehaviour
{
    [SerializeField] Communication communication;

    private void Awake()
    {
        communication = Instantiate(communication);
        communication.transform.parent = this.transform;
    }



}
