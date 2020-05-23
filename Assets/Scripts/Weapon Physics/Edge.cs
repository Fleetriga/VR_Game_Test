using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    Vector3 direction;
    public Vector3 Direction { get => direction; set => direction = value; }

    public int ID;
    EdgeMaster master;
    bool triggered;

    Transform raycastSource;
    public Transform RaycastSource { get => raycastSource; }

    private void Start()
    {
        master = GetComponentInParent<EdgeMaster>();
        triggered = false;
        raycastSource = transform.GetChild(0).transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }
        if (!triggered)
        {
            master.EdgeTriggered(ID);
            triggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }
        if (triggered)
        {
            master.EdgeExit();
            triggered = false;
        }
    }

}
