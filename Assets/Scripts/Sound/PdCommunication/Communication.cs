using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Communication : MonoBehaviour
{
    protected List<Grain> grains;           //hier raus -> GrainMessage von einem Interface ableiten?
    void Start()
    {
        Assets.Scripts.Core.BallFactory.OnNewBallsCreated += UpdateGrainList;
        grains = SoundSampler.Instance.GetGrains();
        TurnPatchOn();
    }

    public virtual void Update()
    {
        if(grains == null)
        {
            return;
        }

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

    public void UpdateGrainList()
    {
        grains = SoundSampler.Instance.GetGrains();
    }
}
