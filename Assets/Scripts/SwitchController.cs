using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Tooltip("Drag your Main Knob gameobject here")]
    public Transform mainKnob;

    Vector3 downPos;
    Vector3 upPos;
    bool isOn = false;

    // how far (in local Y) you want it to travel
    public float travelDistance = 0.2f;
    
    void Start()
    {
        // store the “off” position
        downPos = mainKnob.localPosition;
        // compute the “on” position
        upPos   = downPos + Vector3.up * travelDistance;
    }

    public void Toggle()
    {
        // flip-flop
        isOn = !isOn;
        mainKnob.localPosition = isOn ? upPos : downPos;
        // TODO: fire your power-on logic here
    }
}

