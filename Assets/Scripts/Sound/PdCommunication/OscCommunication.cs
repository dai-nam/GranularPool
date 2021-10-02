using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo OscCommunication und PdCommunication ableiten von Interface ICommunication
public class OscCommunication : Communication
{
    OSC osc;

    public void Awake()
    {
        osc = GetComponent<OSC>();
    }

    public override void SendData(GrainMessage message)
    {
        //message.PrintMessage();

        //  SendId(message);
        // SendPosition(message);
        // SendLength(message);
        SendOscMessage(message);
    }

    private void SendOscMessage(GrainMessage message)
    {
        OscMessage oscMsg = new OscMessage();
        //oscMsg.address = "/GrainData/" + message.id;
         oscMsg.address = "/GrainData";
        oscMsg.values.Add(message.id);
        oscMsg.values.Add(message.enabled); // -> todo Event
        oscMsg.values.Add(message.position);
        oscMsg.values.Add(message.length);
        osc.Send(oscMsg);
        //print(oscMsg);
    }
    private void SendId(GrainMessage message)
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/id";
        oscMsg.values.Add(message.id);
        osc.Send(oscMsg);
        print(oscMsg);

    }

    private void SendPosition(GrainMessage message)
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/position";
        oscMsg.values.Add(message.position);
        osc.Send(oscMsg);

    }

    private void SendLength(GrainMessage message)
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/length";
        oscMsg.values.Add(message.length);
        osc.Send(oscMsg);
    }

    private void SendActiveState(GrainMessage message)
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/enabled";
        oscMsg.values.Add(message.enabled);
        osc.Send(oscMsg);
    }

    public override void TurnPatchOn()
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/enable";
        oscMsg.values.Add(1);
        osc.Send(oscMsg);
    }

    public override void TurnPatchOff()
    {
        OscMessage oscMsg = new OscMessage();
        oscMsg.address = "/disable";
        oscMsg.values.Add(0);
        osc.Send(oscMsg);
    }
}
