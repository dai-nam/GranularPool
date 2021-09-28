using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    private Vector3 center;
    public float radius;

    public static Table Instance;

    private void Awake()
    {
        Instance = this;
        center = new Vector3(transform.position.x, 0f, transform.position.z);
        radius = transform.localScale.x / 2f;
    }

    public bool CheckIfOnTable(Vector3 pos)
    {
        return Vector3.Distance(center, pos) < radius;
    }

    public Vector3 GetRandomPositionOnTable()
    {
        Vector2 randomyXZ = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float scale = Random.Range(0f, radius);
        randomyXZ.Scale(new Vector2(scale, scale));
        Vector3 randomVector = new Vector3(randomyXZ.x, 2.5f, randomyXZ.y);
        return randomVector;
    }

    public Vector2 GetCenterXandZposition()
    {
        return new Vector2(center.x, center.z);
    }
}
