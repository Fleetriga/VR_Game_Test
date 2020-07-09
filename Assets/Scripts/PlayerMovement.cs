using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;

    TeleportEffects teleportEffects;
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        teleportEffects = GetComponent<TeleportEffects>();
        characterController.detectCollisions = false;
        VRInputs.VRInputsInstance.TriggerDown += Dash;
    }

    private void Dash(Hand source, EventArgs args)
    {
        teleportEffects.StartEffects();

        //Get direction
        Vector3 direction = source.transform.forward;
        direction.y = 0;

        //Dash distance in that direction FIRST IN XZ, Y later
        characterController.Move(direction * 3);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));

        characterController.Move(-new Vector3(0, 9.81f, 0) * Time.deltaTime);

    }
}
