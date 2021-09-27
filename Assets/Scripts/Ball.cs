using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] public bool isRespawnable;
    [SerializeField] float size = 1;
    public bool onTable;
    Rigidbody rb;
    Table table;

    private void Awake()
    {
        table = Table.Instance;
        onTable = true;
        rb = GetComponent<Rigidbody>();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        onTable = table.CheckIfOnTable(transform.position);
        if (!onTable)
        {
            DecreaseVelocity();
        }
    }



    private void DecreaseVelocity()
    {
        rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f, 1f, 0.95f));
        //  rb.velocity = Vector3.Scale(rb.velocity, new Vector3(Time.deltaTime, 1f, Time.deltaTime));    //Bug
    }

    public void Respawn()
    {
        Vector3 randomPosition = table.GetRandomPositionOnTable();
        RespawnAtPosition(randomPosition);
        rb.velocity = new Vector3(0, 0, 0);
    }



    private void RespawnAtPosition(Vector3 randomPosition)
    {
        transform.position = randomPosition;
    }
}
