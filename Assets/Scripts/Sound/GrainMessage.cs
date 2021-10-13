using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainMessage
{
    public int id;
    public float position;
    public float length;
    public float enabled;
    public int sampleId;



    public GrainMessage(int _id, float _position, float _length, bool _enabled, int sampleId)
    {
        
        SetMessage(_id, _position, _length, _enabled, sampleId);
    }

   
    public void SetMessage(int _id, float _position, float _length, bool _enabled, int _sampleId)
    {
        this.id = _id;
        this.position = _position;
        this.length = _length;
        this.enabled = IsEnabled(_enabled);
        this.sampleId = _sampleId;
    }

    private float IsEnabled(bool active)
    {
        return active ? 1f : 0f;
    }


    public void PrintMessage()
    {
        string msg = string.Format("ID: {0}, Position: {1}, Length: {2}, Enabled: {3}, SampleId: {4}", id, position, length, enabled, sampleId);
        Debug.Log(msg);
    }

}
