using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainDisplay : MonoBehaviour
{
    public uint length;

    private void Start()
    {
        AudioclipLoader acl = FindObjectOfType<AudioclipLoader>();
        length = (uint) acl.clip.length * 1000;
    }


}
