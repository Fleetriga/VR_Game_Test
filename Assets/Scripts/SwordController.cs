using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordController : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip phaseSound;
    [SerializeField] AudioClip bindingSound;
    [SerializeField] WeaponStatsAsset startStats;
    private Interactable interactable;
    WeaponStats weaponStats;

    //Teleporting
    private bool teleporting;

    //Binding
    bool swordBound;
    bool binding;

    //Attacking
    bool damageAllowed;
    float currentResistance;
    //Time based damage
    [SerializeField] float damageCooldown = 0.1f;
    //float currentDamageCooldownStart = 0f;
    //Distance based damage
    float distanceFromLastDamage = 0f;
    Vector3 lastDamagingStrikeLocation;
    Vector3 currentStrikeLocation;

    EdgeMaster edgeMaster;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        edgeMaster = GetComponentInChildren<EdgeMaster>();
        VRInputs.VRInputsInstance.SideGripDown += OnGripDown;
        teleporting = false;
        damageAllowed = true;
        weaponStats = new WeaponStats(startStats);

        //Debug purposed just bind it at the start
        //BindSword();
    }

    private void Update()
    {
        //For timing based damage
        //if (!damageAllowed && (Time.time - currentDamageCooldownStart) > damageCooldown)
        //{
        //    damageAllowed = true;
        //}

        //Set distance from last damage
        distanceFromLastDamage = Vector3.Distance(currentStrikeLocation, lastDamagingStrikeLocation);

        //For distance based damage
        if (!damageAllowed && distanceFromLastDamage >= damageCooldown)
        {
            damageAllowed = true;
        }
    }


    #region Weapon Attachment

    public void OnGripDown(Hand source, EventArgs args)
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
            StartCoroutine(BindSword());
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
    
    IEnumerator BindSword()
    {
        binding = true;
        audioSource.clip = bindingSound;
        audioSource.Play();
        yield return new WaitForSeconds(3.8f);

        swordBound = true;
        transform.GetChild(4).GetChild(0).GetComponent<ParticleSystem>().Play();
    }

    public void SwitchLayer(int givenLayer)
    {
        gameObject.layer = givenLayer;
        foreach (Transform t in transform) { t.gameObject.layer = givenLayer; }
    }

    #endregion


    #region Weapon Damage

    //Cause Damage to the enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }
        if (other.GetComponentInParent<NonPlayerCharacter>().IsDead) { ConveyForce(other); return; }

        if (damageAllowed)
        {
            OnCauseDamage();
            lastDamagingStrikeLocation = ContactPoint();
            other.GetComponentInParent<NonPlayerCharacter>().TakeDamage(weaponStats, ContactPoint());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }
        if (other.GetComponentInParent<NonPlayerCharacter>().IsDead)
        {
            if (interactable.attachedToHand != null)
            {
                ConveyForce(other);
            }
            return;
        }

        if (currentResistance <= weaponStats.Slice)
        {
            //Time based
            currentResistance += other.GetComponent<BodyPart>().Resistance * Time.deltaTime;
            //Distance based
            currentStrikeLocation = ContactPoint();

            if (damageAllowed)
            {
                other.GetComponentInParent<NonPlayerCharacter>().TakeSliceDamage(weaponStats, ContactPoint());

                OnCauseDamage();
            }
        }
        else
        {
            damageAllowed = false;
            //Phase weapon
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 9) { return; }

        currentResistance = 0;
        damageAllowed = true;
    }

    void ConveyForce(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(interactable.attachedToHand.GetTrackedObjectVelocity() * 50, ForceMode.Impulse);
    }

    void OnCauseDamage()
    {
        damageAllowed = false;
        lastDamagingStrikeLocation = ContactPoint();
    }

    Vector3 ContactPoint()
    {
        //Find point of contact, hitting only NPCs.
        RaycastHit hit;
        LayerMask lm = 1 << 9;

        Physics.Raycast(edgeMaster.CurrentEdgesRaycastSource.position, edgeMaster.CurrentEdgesRaycastSource.right, out hit, Mathf.Infinity, lm);

        return hit.point;
    }
    
    #endregion
}
