using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainDisplay : MonoBehaviour
{
    public uint length;

    private void Start()
    {
        AudioClip clip = AudioLoader.Instance.audioClip; 
        length = (uint) clip.length * 1000;
    }


}
