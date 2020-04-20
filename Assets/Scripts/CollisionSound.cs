using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] AudioSource impactSource;
    [SerializeField] AudioClip impactSound;

    public bool OutputAllowed = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1 && OutputAllowed)
        {
            impactSource.clip = impactSound;
            impactSource.Play();
        }
    }
}
