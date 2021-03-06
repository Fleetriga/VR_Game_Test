﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRInputs : MonoBehaviour
{

    [SerializeField] Hand[] hands;

    public static VRInputs VRInputsInstance = null;

    //Input - Grip down
    [SerializeField] SteamVR_Action_Boolean gripInput;
    public delegate void SideGripDownEventHandler(Hand source, EventArgs args);
    public event SideGripDownEventHandler SideGripDown;

    //Input - Trigger
    [SerializeField] SteamVR_Action_Boolean triggerInput;
    public delegate void TriggerDownEventHandler(Hand source, EventArgs args);
    public event TriggerDownEventHandler TriggerDown;

    //Input - Trackpad north (down and up)
    [SerializeField] SteamVR_Action_Boolean trackPadNorthInput;
    public delegate void TrackPadNorthEventHandler(Hand source, EventArgs args);
    public event TrackPadNorthEventHandler TrackPadNorthDown;
    public event TrackPadNorthEventHandler TrackPadNorthUp;


    void Awake()
    {
        if (VRInputsInstance == null)
        {
            VRInputsInstance = this;
        }
        else if (VRInputsInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetHandVelocity()
    {
        return new Vector3();
    }


    // Update is called once per frame
    void Update()
    {
        //Check either left or right hand
        // Side grip
        if (gripInput[hands[0].handType].stateDown)
        {
            SideGripDown?.Invoke(hands[0], EventArgs.Empty);
        }
        if (gripInput[hands[1].handType].stateDown)
        {
            SideGripDown?.Invoke(hands[1], EventArgs.Empty);
        }

        //Track pad
        if (triggerInput[hands[0].handType].stateDown)
        {
            TriggerDown?.Invoke(hands[0], EventArgs.Empty);
        }
        if (triggerInput[hands[1].handType].stateDown)
        {
            TriggerDown?.Invoke(hands[1], EventArgs.Empty);
        }

        //Trigger
        if (triggerInput[hands[0].handType].stateDown || trackPadNorthInput[hands[1].handType].stateDown)
        {
            TrackPadNorthDown?.Invoke(hands[1], EventArgs.Empty);
        }
        if (trackPadNorthInput[hands[0].handType].stateUp || trackPadNorthInput[hands[1].handType].stateUp)
        {
            TrackPadNorthUp?.Invoke(hands[1], EventArgs.Empty);
        }
    }



}
