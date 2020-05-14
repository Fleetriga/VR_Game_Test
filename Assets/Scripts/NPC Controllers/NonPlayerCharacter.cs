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

    void Awake()
    {
        realStats = new CharacterStats(startingStats);
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
    }

    public void TakeSliceDamage(Stats damageSource, Vector3 damageLoc)
    {
        if (trackedSliceDamage == null) { return; } //Stop same frame double damage

        realStats.Health -= damageSource.Slice;
        currentSliceDamage += damageSource.Slice;
        HealthValueChanged?.Invoke(realStats.HealthPct);

        trackedSliceDamage.UpdatePositionAndValue(damageLoc, currentSliceDamage.ToString());
    }


}
