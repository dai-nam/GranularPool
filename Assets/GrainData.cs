using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainData : MonoBehaviour
{
   // private static uint id;
   // public uint instanceId;
    public uint grainLength;
    public float grainWidth;     //width of Grain in Sampler Display
    public float grainPosition;
    public int id;


    void Start()
    {
      //  transform.parent = UiAudioManager.Instance.transform;

        //    instanceId = id++;
        GetComponent<ClippingHandler>().enabled = true;
    }

    void Update()
    {
        UpdateGrainData();
    }

    private void UpdateGrainData()
    {
        if (UiAudioManager.Instance.numGrains <= 0)
            return;

        // grainLength = UiAudioManager.Instance.receivedLengths[id]
        // grainPosition = UiAudioManager.Instance.receivedPositions[id];
        grainLength = UiAudioManager.Instance.lengths[id];
        grainPosition = UiAudioManager.Instance.positions[id];

        grainWidth = (float) grainLength / (float) UiAudioManager.Instance.samplerLength;
    }

    public void SetId(int id_)
    {
        this.id = id_;
    }
}
