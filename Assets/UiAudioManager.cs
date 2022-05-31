using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAudioManager : MonoBehaviour
{
    private List<Grain> sentGrainData;
    public float[] receivedPositions;
    public uint[] receivedLengths;
    public uint length;
    private static UiAudioManager _instance;
    public static UiAudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("AudioLoader");
                go.AddComponent<UiAudioManager>();
            }
            return _instance;
        }
    }

    private void Start()
    {
        _instance = this;
        AudioClip clip = AudioLoader.Instance.audioClips[0];    //todo für mehrere Clips
        length = (uint)clip.length * 1000;
    }

    void Update()
    {
        ReceiveSamplerData();
    }

    void ReceiveSamplerData()
    {
        sentGrainData = SoundSampler.Instance.GetGrains();      //to refactor: nicht jeden Frame.. Event wenn sich Bälle ändern
        int numGrains = sentGrainData.Count;
        receivedPositions = new float[numGrains];
        receivedLengths = new uint[numGrains];

        for (int i = 0; i < numGrains; i++)
        {
            receivedPositions[i] = sentGrainData[i].GetGrainPosition();
            receivedLengths[i] = (uint)sentGrainData[i].GetGrainLength();
        }
    }

}
