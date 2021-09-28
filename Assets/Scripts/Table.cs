using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    private Vector2 center;
    private float radius;
    float tableSize = 58f;

    public static Table Instance;

    private void Awake()
    {
        Instance = this;
        center = new Vector2(transform.position.x, transform.position.y);
        radius = transform.localScale.x / 2f;
    }

    /*
    public bool OnTable(Vector2 pos)
    {
       return Vector2.Distance(center, pos) < radius;
    }
    */

    public bool CheckIfOnTable(Vector3 pos)
    {
        return (pos.x > -tableSize && pos.x < tableSize &&
                pos.z > -tableSize && pos.z < tableSize);
    }

    public Vector3 GetRandomPositionOnTable()
    {
        float offset = 5f;
        return new Vector3(Random.Range(-tableSize+offset, tableSize-offset), 2.5f, Random.Range(-tableSize+offset, tableSize-offset));
    }

}
