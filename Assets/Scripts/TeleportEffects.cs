using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportEffects : MonoBehaviour
{
    [SerializeField] Renderer cameraFeedPlane;
    [SerializeField] GameObject cameraFeed;
    [SerializeField] GameObject actualCamera;

    AudioSource audio;
    [SerializeField] AudioClip teleportClip;

    [SerializeField] float teleportTime;
    float startTeleportTime;

    bool teleporting;

    void Start()
    {
        this.cameraFeedPlane.sharedMaterial.SetFloat("Vector1_3CFA44D2", 1.5f);
        audio = GetComponent<AudioSource>();
        audio.clip = teleportClip;
        teleporting = true;
    }

    public void StartEffects()
    {
        //Reposition the camera
        cameraFeed.transform.position = actualCamera.transform.position;
        cameraFeed.transform.rotation = actualCamera.transform.rotation;

        //Play teleport sound
        audio.Play();

        //Start corroutine to disolve the plane
        StartCoroutine(DisolveCameraFeed());
    }

    void Update()
    {
        cameraFeedPlane.sharedMaterial.SetFloat("Vector1_3CFA44D2", (((Time.time - startTeleportTime) / teleportTime) * 1.5f)); //Don't want it stuck on the screen
        if (teleporting)
        {
            cameraFeed.transform.rotation = actualCamera.transform.rotation;
            //cameraFeed.transform.position = actualCamera.transform.position;
        }
    }

    IEnumerator DisolveCameraFeed()
    {
        //cameraFeedPlane.sharedMaterial.SetFloat("Vector1_3CFA44D2", 0);
        //yield return new WaitForSeconds(3);

        teleporting = true;
        startTeleportTime = Time.time;
        cameraFeedPlane.sharedMaterial.SetFloat("Vector1_3CFA44D2", 0);
        yield return new WaitForSeconds(teleportTime);

        teleporting = false;
    }
}
