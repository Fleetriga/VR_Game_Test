using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EdgeMaster : MonoBehaviour
{
    //1. point 2. left 3. right 4. tang
    [SerializeField] Edge[] Edges;
    [SerializeField] GameObject debugDirecitonIndicator;
    Vector3 edgeDirection;
    public Vector3 EdgeDirection { get => edgeDirection; set => edgeDirection = value; }

    Transform currentEdgesRaycastSource;
    public Transform CurrentEdgesRaycastSource { get => currentEdgesRaycastSource; }

    public void EdgeTriggered(int ID)
    {
        foreach (Edge e in Edges)
        {
            if (e.ID != ID) { e.gameObject.SetActive(false); }
        }

        Hand possiblyAttached = GetComponentInParent<Interactable>().attachedToHand;
        if (possiblyAttached == null) { edgeDirection = GetComponentInParent<Rigidbody>().velocity.normalized; }
        else { edgeDirection = possiblyAttached.GetTrackedObjectVelocity().normalized; }

        currentEdgesRaycastSource = Edges[ID].RaycastSource;
    }

    public void EdgeExit()
    {
        foreach (Edge e in Edges)
        {
            e.gameObject.SetActive(true);
        }
        debugDirecitonIndicator.SetActive(false);
        currentEdgesRaycastSource = null;
    }
}
