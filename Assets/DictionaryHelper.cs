using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DictionaryHelper
{
    public int key;
    public float value;
    public DictionaryHelper(int t, float u)
    {
        key = t;
        value = u;
    }
}
