
using UnityEngine;
using Assets.Scripts.InGameObjects;


    public class Grain : MonoBehaviour
{
    [SerializeField] float grainPosition;
    [SerializeField] float grainLength;
    [SerializeField] int grainId;
    [SerializeField] int grainSampleId;

    [SerializeField] bool grainActive;

    [SerializeField] public Ball connectedBall;
    public GrainMessage grainMessage;

    private void Start()
    {
        grainMessage = new GrainMessage(0, 0, 0, true, 0);   //todo
    }


    public void SetGrainId(int id)
    {
        this.grainId = id;
    }

    public int GetGrainId()
    {
        return grainId;
    }

    public void SetGrainSampleId(int sid)
    {
        this.grainSampleId = sid;
    }

    public int GetGrainSampleId()
    {
        return grainSampleId;
    }

    public void SetGrainPosition(float position)
    {
        grainPosition = position;
    }

    internal void SetGrainLength(float length)
    {
        grainLength = length;
    }

    public float GetGrainPosition()
    {
        return grainPosition;
    }

    public float GetGrainLength()
    {
        return grainLength;
    }

    internal void SetConnectedBall(Ball ball)
    {
        this.connectedBall = ball;
    }

    public void Update()
    {
        if(grainActive)
        {
            UpdatePosition();
            UpdateLength();
        }

        grainActive = IsActive();
        UpdateMessage();
    }

    private void UpdatePosition()
    {
        float value = SoundSampler.Instance.ConvertBallPositionToGrainPosition(connectedBall.GetXandZposition());
        SetGrainPosition(value);
    }

    private void UpdateLength()
    {
        float value = SoundSampler.Instance.ConvertBallPositionToGrainLength(connectedBall.GetXandZposition());
        SetGrainLength(value);
    }

    //todo: message aus diesre Klasse raus
    public void UpdateMessage()
    {
        grainMessage.SetMessage(grainId, grainPosition, grainLength, grainActive, grainSampleId);
    }

    public GrainMessage GetGrainMessage()
    {
        return grainMessage;
    }

    public bool IsActive()
    {
        return connectedBall.onTable;
    }

}
