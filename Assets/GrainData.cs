using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainData : MonoBehaviour
{
    private static uint id;
    public uint instanceId;
    public float sampleLength;
    public float grainLength;
    public float grainPosition;


    void Start()
    {
        instanceId = id++;     
    }

    // Update is called once per frame
    void Update()
    {
        sampleLength = UiAudioManager.Instance.length;
        grainLength = UiAudioManager.Instance.receivedLengths[instanceId];
        grainPosition = UiAudioManager.Instance.receivedPositions[instanceId];
    }
}
