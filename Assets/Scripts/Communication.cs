﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Communication : MonoBehaviour
{
    protected List<Grain> grains;
    void Start()
    {
        grains = SoundSampler.Instance.GetGrains();
        TurnPatchOn();
    }

    public virtual void Update()
    {
        foreach(Grain grain in grains)
        {
            GrainMessage message = grain.GetGrainMessage();
            SendData(message);
        }  
    }

    public void OnDisable()
    {
        TurnPatchOff();
    }

    public abstract void SendData(GrainMessage message);

    public abstract void TurnPatchOn();
    public abstract void TurnPatchOff();

}