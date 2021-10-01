using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PdCommunication : Communication
{
   [SerializeField] LibPdInstance pdInstance;

    public override void SendData(GrainMessage message)
    {
        /*
        pdInstance.SendFloat("id" + grain.GetGrainId(), grain.GetGrainId());
        //  pdInstance.SendFloat("position", grain.GetGrainPosition());
        pdInstance.SendFloat("length", grain.GetGrainLength());
        pdInstance.SendFloat("start", grain.GetGrainPosition());
        // pdInstance.SendFloat("position"+grain.GetGrainId(), grain.GetGrainPosition());
        // pdInstance.SendFloat("length" + grain.GetGrainId(), grain.GetGrainLength());
        */
    }

    public override void TurnPatchOn()
    {
        throw new System.NotImplementedException();
    }
    public override void TurnPatchOff()
    {
        throw new System.NotImplementedException();
    }
}
