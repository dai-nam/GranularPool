using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayheadController : MonoBehaviour
{
    [Range(-50, 50)] [SerializeField] float speed = 20f;
    [Range(0, 1)] public float progress = 0;
    RectTransform rt;
    RectTransform parentRectangle;
    Vector3 startPosition; //refactor: beide Positionen im parent hinterlegen
    Vector3 endPosition;
    Vector3 prevParentPosition; //irgendwie in die Methode verlegen
    public bool clipped = false;
    Vector3 dummie;
    Vector3 offset;

[SerializeField] ClippedArea clippedAreaLeft;
    [SerializeField] ClippedArea clippedAreaRight;


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
        dummie = startPosition;
        print("Start " + startPosition);
        offset = new Vector3();
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

        // rt.position += Vector3.right * Time.deltaTime * speed;
     //   CheckAndApplyWrapAround();
        Test3();
    }

    //todo: nicht von Position abhängig machen, sondern voon Fortschritt in Prozent. Sonst Schwierigkeiten beim WrapAround wenn mainArea clippt
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

    void Test()
    {
        progress += Time.deltaTime /2f; //todo: von Grain Länge abhängig machen

        Vector3 offset = (Vector3.right * parentRectangle.GetComponent<GrainRectangle>().grainWidthInPixels * progress);

      //  print("rtposx: " + rt.position.x + "   parentx0: " + parentRectangle.GetComponent<GrainRectangle>().GetWorldCornersOfSampler()[0].x + 
       //     "parentx2: " + parentRectangle.GetComponent<GrainRectangle>().GetWorldCornersOfSampler()[2].x);

        if (rt.position.x > parentRectangle.GetComponent<GrainRectangle>().GetWorldCornersOfSampler()[2].x)
        {
            print("CLIP");
            clipped = true;
        }
       if (!clipped)
        {
            print("!clipped");
            rt.position = startPosition + offset;
        }
        else 
            {
            print("else");
                rt.position = parentRectangle.GetComponent<GrainRectangle>().GetWorldCornersOfSampler()[0] + offset;
            }
        
        if (progress >= 1f)
        {
            progress = 0;
            clipped = false;
        }
    }
    void Test2()
    {
        startPosition = parentRectangle.GetComponent<GrainRectangle>().GetWorldCornersOfRectangle()[1];

        Vector3[] samplerCorners = new Vector3[4];
        UiAudioManager.Instance.GetComponent<RectTransform>().GetWorldCorners(samplerCorners);

        //progress += Time.deltaTime; //todo: von Grain Länge abhängig machen
        float speed = 0.004f;
        offset = offset + (Vector3.right * parentRectangle.GetComponent<GrainRectangle>().grainWidthInPixels *  speed);
        float progress = offset.x / parentRectangle.GetComponent<GrainRectangle>().grainWidthInPixels;

         //dummie += (Vector3.right * 5f);
        dummie += offset;
     
        print("Dummie: " + dummie.x + "   Left Corner: " + samplerCorners[1].x + "   Right Corner: " + samplerCorners[2].x);
        print(UiAudioManager.Instance.GetComponent<RectTransform>().position.x + UiAudioManager.Instance.GetComponent<RectTransform>().rect.width);

        //  if (dummie.x > UiAudioManager.Instance.GetComponent<RectTransform>().position.x + UiAudioManager.Instance.GetComponent<RectTransform>().rect.width)
         if (dummie.x > samplerCorners[2].x)
        {

            clipped = true;
        }
        if(clipped)
        {
            Vector3 sampelerRT = new Vector3(samplerCorners[1].x, samplerCorners[1].y, 0);
            rt.position = sampelerRT;
        }
        else
        {
            rt.position = startPosition;
        }
       // rt.position += (Vector3.right*5);
        rt.position += offset;

        if (progress >= 1f)
        {
            progress = 0f;
            clipped = false;
            dummie = startPosition;
            offset = new Vector3();
        }
    }

    void Test3()
    {
        progress += Time.deltaTime / 5; //todo: von Grain Länge abhängig machen

        Vector3[] samplerCorners = new Vector3[4];
        UiAudioManager.Instance.GetComponent<RectTransform>().GetWorldCorners(samplerCorners);
        Vector3 offset = (Vector3.right * parentRectangle.GetComponent<GrainRectangle>().grainWidthInPixels * progress);
        rt.position = startPosition + offset;
        dummie = startPosition + offset;
        if(dummie.x > samplerCorners[2].x)
        {
            clipped = true;
        }
        if(clipped)
        {
            float newX = (rt.position.x % samplerCorners[2].x) + samplerCorners[0].x;
            rt.position = new Vector3(newX, rt.position.y, 0);
        }


        if (progress >= 1f)
        {
            progress = 0f;
            clipped = false;
            dummie = startPosition;
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
