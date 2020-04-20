using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordController : MonoBehaviour
{

    [SerializeField] Renderer renderer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip phaseSound;
    [SerializeField] WeaponStatsAsset startStats;
    private Interactable interactable;
    WeaponStats weaponStats;

    Boolean swordBound;
    private bool teleporting;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        VRInputs.VRInputsInstance.TriggerDown += OnTriggerDown;
        teleporting = false;
        weaponStats = new WeaponStats(startStats);
    }

    // Update is called once per frame
    void Update()
    {
        float temp = renderer.sharedMaterial.GetFloat("Vector1_3CFA44D2") + (renderer.sharedMaterial.GetFloat("Vector1_D1F593F7") * (teleporting ? 1 : -1) * Time.deltaTime);
        if (temp > 1) { temp = 1; }
        if (temp < 0) { temp = 0; }

        renderer.sharedMaterial.SetFloat("Vector1_3CFA44D2", temp);
    }

    public void OnTriggerDown(Hand source, EventArgs args)
    {
        //switch (swordBound, interactable.attachedToHand == null) 
        //{
        //    case (true, true) : TeleportBackToHand(); break;
        //    case (false, true): BindSword(); break;
        //    case (_, _): break;
        //};

        if (swordBound && interactable.attachedToHand == null)
        {
            StartCoroutine(TeleportBackToHand(source));
        }
        else if (!swordBound && interactable.attachedToHand != null)
        {
            BindSword();
        }
    }

    private IEnumerator TeleportBackToHand(Hand hand)
    {
        if (interactable.attachedToHand == null && !teleporting)
        {
            GetComponent<CollisionSound>().OutputAllowed = false;
            teleporting = true;
            audioSource.clip = phaseSound;
            audioSource.Play();
            yield return new WaitForSeconds(renderer.sharedMaterial.GetFloat("Vector1_D1F593F7"));

            //teleport
            teleporting = false;
            GetComponent<Rigidbody>().useGravity = false;
            hand.AttachObject(gameObject, hand.GetBestGrabbingType());
            GetComponent<Rigidbody>().isKinematic = false;
            audioSource.clip = phaseSound;
            audioSource.Play();
            yield return new WaitForSeconds(renderer.sharedMaterial.GetFloat("Vector1_D1F593F7"));

            //End Sequence
            GetComponent<CollisionSound>().OutputAllowed = true;
            GetComponent<Rigidbody>().useGravity = true;
            teleporting = false;
        }
    }

    private void BindSword()
    {
        swordBound = true;
        //some effects
    }

    public void SwitchLayer(int givenLayer)
    {
        gameObject.layer = givenLayer;
        foreach (Transform t in transform) { t.gameObject.layer = givenLayer; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) 
        {
            collision.gameObject.GetComponentInParent<NonPlayerCharacter>().TakeDamage(weaponStats);
        }
    }

}
