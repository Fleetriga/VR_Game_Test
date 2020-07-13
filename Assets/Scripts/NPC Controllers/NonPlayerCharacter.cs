using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] CharacterStatsAsset startingStats;
    [SerializeField] CharacterStats realStats;

    //Damage Texts
    [SerializeField] GameObject damageTextPrefab;
    UI_DamageText trackedSliceDamage;
    float currentSliceDamage;

    public event Action<float> HealthValueChanged = delegate { };
    public bool IsDead => realStats.Health <= 0;

    static Animator anim;

    void Awake()
    {
        realStats = new CharacterStats(startingStats);
        anim = GetComponentInChildren<Animator>();
        SetNPCRigidbodies(true);
        SetNPCColliders(false);
    }

    void Update()
    {

    }

    public void TakeDamage(Stats damageSource, Vector3 damageLoc)
    {
        currentSliceDamage = 0;

        GameObject temp = Instantiate(damageTextPrefab);
        trackedSliceDamage = temp.GetComponent<UI_DamageText>();

        //Basic for testing. Will be broadened.
        realStats.Health -= damageSource.Attack;
        HealthValueChanged?.Invoke(realStats.HealthPct);

        temp.GetComponent<UI_DamageText>().UpdatePositionAndValue(damageLoc, damageSource.Attack.ToString());

        currentSliceDamage = damageSource.Attack;

        if (realStats.Health <= 0) { Die(); }
    }

    public void TakeSliceDamage(Stats damageSource, Vector3 damageLoc)
    {
        if (trackedSliceDamage == null) { return; } //Stop same frame double damage

        realStats.Health -= damageSource.Slice;
        currentSliceDamage += damageSource.Slice;
        HealthValueChanged?.Invoke(realStats.HealthPct);

        trackedSliceDamage.UpdatePositionAndValue(damageLoc, currentSliceDamage.ToString());

        if (realStats.Health <= 0) { Die(); }
    }

    void SetNPCRigidbodies(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void Die()
    {
        GetComponentInChildren<Animator>().enabled = false;
        SetNPCRigidbodies(false);
        SetNPCColliders(true);

        Destroy(gameObject, 3);
    }

    void SetNPCColliders(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }





}
