using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainMessage
{
    public int id;
    public float position;
    public float length;
    public float enabled;


    public GrainMessage(int _id, float _position, float _length, bool _enabled)
    {
        
        SetMessage(_id, _position, _length, _enabled);
    }

   
    public void SetMessage(int _id, float _position, float _length, bool _enabled)
    {
        this.id = _id;
        this.position = _position;
        this.length = _length;
        this.enabled = IsEnabled(_enabled);
    }

    private float IsEnabled(bool active)
    {
        return active ? 1f : 0f;
    }


    public void PrintMessage()
    {
        string msg = string.Format("ID: {0}, Position: {1}, Length: {2}, Enabled: {3}", id, position, length, enabled);
        Debug.Log(msg);
    }

}
