using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pool : MonoBehaviour
{
    public static UI_Pool Instance;
    public List<GameObject> activeObjects = new List<GameObject>(); //dictionary stattdessen mit id als key
    public List<GameObject> poolList = new List<GameObject>();          //temporyry, only for Debugging in Inspector

    public Queue<GameObject> pool = new Queue<GameObject>();


    void Start()
    {
        Instance = this;
        InitializePool();
      //  Assets.Scripts.Core.Initializer.OnCreateNewBalls += ResetPoolAndActiveList;
    }


    void InitializePool()
    {
        //  for(int i = 0; i < transform.childCount; i++)
        for(int i = 0; i < Assets.Scripts.Core.GameManager.Instance.ballCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.name = "Grain UI_" + i;
            pool.Enqueue(go);
            poolList.Add(go);
        }
    }

    public void UpdatePoolList()
    {
        poolList.Clear();
        foreach(GameObject go in pool)
        {
            poolList.Add(go);
        }
    }

    public void ResetPoolAndActiveList()
    {
        pool.Clear();
        poolList.Clear();
        foreach(GameObject go in activeObjects)
        {
            pool.Enqueue(go);
            poolList.Add(go);
            activeObjects.Remove(go);
        }
    }
}
