using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveData : MonoBehaviour
{
    public List<Grain> sentGrainData; 
    public float[] receivedPositions;
    public uint[] receivedLengths;


    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Receive();
    }

    void Receive()
    {
        
            sentGrainData = SoundSampler.Instance.GetGrains();      //to refactor: nicht jeden Frame.. Event wenn sich Bälle ändern
            receivedPositions = new float[sentGrainData.Count];
            receivedLengths = new uint[sentGrainData.Count];


        for (int i = 0; i < sentGrainData.Count; i++)
        {
            receivedPositions[i] = sentGrainData[i].GetGrainPosition();
            receivedLengths[i] = (uint) sentGrainData[i].GetGrainLength();

        }
    }
}
