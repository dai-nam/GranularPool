using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playhead : MonoBehaviour
{
    [Range(0, 1)] public float pos;
    [SerializeField] float speed = 1f;
    GrainAreaDisplay parent;
    Vector3 playheadStartPosition; //refactor: beide Positionen im parent hinterlegen
    Vector3 playheadEndPosition;

    Vector3 prevParentPosition; //irgendwie in die Methode verlegen

    private void Awake()
    {
        parent = GetComponentInParent<GrainAreaDisplay>();
    }

    private void Start()
    {
        playheadStartPosition = parent.transform.position;
        playheadEndPosition = parent.transform.position + Vector3.right * parent.grainAreaWidth;
        SetPosition(playheadStartPosition);
        prevParentPosition = playheadStartPosition;
    }

    void Update()
    {
        UpdatePosition();
    }

    void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    void UpdatePosition()
    {
        FollowParentPosition();
        //transform.position += Vector3.right*Time.deltaTime*speed;
        speed = CalculateMoveSpeed(); //refactor; MovSpeed nucht jeden Frame berechnen, event-basiert
        transform.position += Vector3.right * speed;
        CheckAndApplyWrapAround();
    }

    private void CheckAndApplyWrapAround()
    {
        if(transform.position.x > playheadEndPosition.x)
        {
            transform.position = playheadStartPosition;
        }
        else if(transform.position.x < playheadStartPosition.x)
        {
            transform.position = playheadEndPosition;
        }
    }

    private void FollowParentPosition()
    {
        if(Vector3.Distance(prevParentPosition, parent.transform.position) > 0.00001f)
        {
            transform.position = parent.transform.position;
            playheadStartPosition = parent.transform.position;
            playheadEndPosition = parent.transform.position + Vector3.right * parent.grainAreaWidth;
        }
        prevParentPosition = parent.transform.position;
    }

    //Calculates Speed based on Sample Length
    private float CalculateMoveSpeed()
    {
        float sampleLengthInSeconds = FindObjectOfType<AudioclipLoader>().clip.length;
        float displayWidth = transform.parent.GetComponent<GrainAreaDisplay>().waveFormwidth;
        //move displayWidth in sampleLengthInSeconds
        float speed = (displayWidth / sampleLengthInSeconds) * Time.deltaTime;
        return speed;

    }
}
