using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayheadController : MonoBehaviour
{
    [Range(-50, 50)] [SerializeField] float speed = 20f;
    RectTransform rt;
    RectTransform parentRectangle;
    Vector3 startPosition; //refactor: beide Positionen im parent hinterlegen
    Vector3 endPosition;
    Vector3 prevParentPosition; //irgendwie in die Methode verlegen

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        parentRectangle = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateStartAndEndPositions();
        SetPosition(startPosition);
        prevParentPosition = startPosition;
    }

    void Update()
    {
        UpdatePosition();
    }

    void SetPosition(Vector3 pos)
    {
        rt.position = pos;
    }

    void UpdatePosition()
    {
        FollowParentPosition();
       // speed = CalculateMoveSpeed(); //refactor; MovSpeed nucht jeden Frame berechnen, event-basiert
       
        rt.position += Vector3.right * Time.deltaTime * speed;
        CheckAndApplyWrapAround();
    }

    private void CheckAndApplyWrapAround()
    {
        if (rt.position.x > endPosition.x)
        {
            rt.position = startPosition;
        }
        // "+" bzw. "-" rt.rect.width, weil Pivot vom Playhead an der linken Kante liegt
        else if (rt.position.x + rt.rect.width < startPosition.x)   
        {
            rt.position = endPosition - (Vector3.right * rt.rect.width);
        }
    }

    private void FollowParentPosition()
    {
        if (Vector3.Distance(prevParentPosition, parentRectangle.position) > 0.00001f)
        {
            //updaten nur nötig, wenn sich Position verändert
            UpdateStartAndEndPositions();
        }
        prevParentPosition = parentRectangle.position;
    }

    private void UpdateStartAndEndPositions()
    {
        startPosition = parentRectangle.position;
        endPosition = parentRectangle.position + Vector3.right * parentRectangle.rect.width;
    }

    //Calculates Speed based on Sample Length
    private float CalculateMoveSpeed()
    {
        AudioClip clip = AudioLoader.Instance.audioClips[0];
        float sampleLengthInSeconds = clip.length;
        float displayWidth = parentRectangle.rect.width;
        //move displayWidth in sampleLengthInSeconds
        float speed = (displayWidth / sampleLengthInSeconds) * Time.deltaTime;
        return speed;

    }
}
