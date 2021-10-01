
using UnityEngine;
using Assets.Scripts.InGameObjects;


    public class Grain : MonoBehaviour
{
    [SerializeField] float grainPosition;
    [SerializeField] float grainLength;
    [SerializeField] int grainId;
    [SerializeField] int grainActive;

    [SerializeField] public Ball ball;
    public GrainMessage grainMessage;

    private void Start()
    {
        grainMessage = new GrainMessage(0, 0, 0, 0);   //todo
    }


    public void SetGrainId(int id)
    {
        this.grainId = id;
    }

    public float GetGrainId()
    {
        return grainId;
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
        this.ball = ball;
    }

    public void Update()
    {
        UpdatePosition();
        UpdateLength();
        UpdateMessage();
        grainActive = isActive();
    }

    private void UpdatePosition()
    {
        float value = SoundSampler.Instance.ConvertBallPositionToGrainPosition(ball.GetXandZposition());
        SetGrainPosition(value);
    }

    private void UpdateLength()
    {
        float value = SoundSampler.Instance.ConvertBallPositionToGrainLength(ball.GetXandZposition());
        SetGrainLength(value);
    }

    //todo: message aus diesre Klasse raus
    public void UpdateMessage()
    {
        grainMessage.SetMessage(grainId, grainPosition, grainLength, grainActive);
    }

    public GrainMessage GetGrainMessage()
    {
        return grainMessage;
    }

    public int isActive()
    {
        if (ball.onTable)
            return 1;
        return 0;
    }

}
