using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] float resistance;
    [SerializeField] float bloodFlow;

    public float Resistance { get => resistance; set => resistance = value; }
    public float BloodFlow { get => bloodFlow; set => bloodFlow = value; }
}
