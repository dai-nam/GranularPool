using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainMessage
{
    public int id;
    public float position;
    public float length;


    public GrainMessage(int _id, float _position, float _length)
    {
        SetMessage(_id, _position, _length);
    }

    public void SetMessage(int _id, float _position, float _length)
    {
        this.id = _id;
        this.position = _position;
        this.length = _length;
    }

    public void PrintMessage()
    {
        string msg = string.Format("ID: {0}, Position: {1}, Length: {2}", id, position, length);
        Debug.Log(msg);
    }

}
