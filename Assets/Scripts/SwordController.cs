using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordController : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip phaseSound;
    [SerializeField] WeaponStatsAsset startStats;
    private Interactable interactable;
    WeaponStats weaponStats;

    //Teleporting
    Boolean swordBound;
    private bool teleporting;

    //Attacking
    bool damageAllowed;
    float currentResistance;
    [SerializeField] float damageCooldown = 0.1f;
    float currentDamageCooldownStart = 0f;

    EdgeMaster edgeMaster;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        edgeMaster = GetComponentInChildren<EdgeMaster>();
        VRInputs.VRInputsInstance.TriggerDown += OnTriggerDown;
        teleporting = false;
        damageAllowed = true;
        weaponStats = new WeaponStats(startStats);
    }

    private void Update()
    {
        if (!damageAllowed && (Time.time - currentDamageCooldownStart) > damageCooldown)
        {
            damageAllowed = true;
        }
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
            StartCoroutine(GetComponentInChildren<DissolveController>().Dissolve());
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
            yield return new WaitForSeconds(GetComponentInChildren<DissolveController>().DisolveTime);

            //teleport
            Destroy(GetComponent<FixedJoint>());
            teleporting = false;
            hand.AttachObject(gameObject, hand.GetBestGrabbingType());
            audioSource.Play();
            yield return new WaitForSeconds(GetComponentInChildren<DissolveController>().DisolveTime);

            //End Sequence
            GetComponent<CollisionSound>().OutputAllowed = true;
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



    //Cause Damage to the enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }

        if (damageAllowed)
        {
            damageAllowed = false;
            currentDamageCooldownStart = Time.time;
            other.GetComponentInParent<NonPlayerCharacter>().TakeDamage(weaponStats, transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }

        currentResistance = 0;
        currentDamageCooldownStart = Time.time;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }

        if (currentResistance <= weaponStats.Slice)
        {
            currentResistance += other.GetComponent<BodyPart>().Resistance * Time.deltaTime;
            if (damageAllowed)
            {
                //Find point of contact.
                RaycastHit hit;
                Physics.Raycast(edgeMaster.CurrentEdgesRaycastSource.position, new Vector3(1,0,0), out hit, Mathf.Infinity, 9);
                other.GetComponentInParent<NonPlayerCharacter>().TakeSliceDamage(weaponStats, hit.point);

                damageAllowed = false;
                currentDamageCooldownStart = Time.time;
            }
        }
        else
        {
            damageAllowed = false;
            //Phase weapon
        }
    }

}
        