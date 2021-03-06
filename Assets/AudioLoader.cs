using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    public const uint numSamples = 1;
    public AudioClip[] audioClips = new AudioClip[numSamples];

    private static AudioLoader _instance;
    public static AudioLoader Instance {
        get
        {
            if(_instance == null){
                GameObject go = new GameObject("AudioLoader");
                go.AddComponent<AudioLoader>();
            }
            return _instance;
        }
    }

    private void OnApplicationQuit()
    {
        print("Destroyedd");
        Destroy(gameObject);
    }


    private void Awake()
    {
        _instance = this;
    }

}
