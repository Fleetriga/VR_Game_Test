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

    [SerializeField] float TeleportDistance = 3;

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

        //Check there's no wall in the way
        RaycastHit raycastHit;
        int layerMask = 1 << 14;
        bool hit = Physics.Raycast(transform.position, direction, out raycastHit, TeleportDistance, layerMask);

        //Disable characterController because this overrides changes to transform
        GetComponent<CharacterController>().enabled = false;

        //Teleport to the wall if hit, otherwise teleport full distance 
        if (hit)
        {
            transform.position = raycastHit.point;
        }
        else
        {
            transform.position = transform.position + (direction * TeleportDistance);
        }
        GetComponent<CharacterController>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));

        characterController.Move(-new Vector3(0, 9.81f, 0) * Time.deltaTime);

    }
}
