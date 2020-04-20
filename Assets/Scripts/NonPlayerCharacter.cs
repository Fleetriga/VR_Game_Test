using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] CharacterStatsAsset startingStats;
    [SerializeField] CharacterStats realStats;

    public event Action<float> HealthValueChanged = delegate { };

    void Awake()
    {
        realStats = new CharacterStats(startingStats);
    }

    void Update()
    {
    }

    public void TakeDamage(Stats damageSource)
    {
        //Basic for testing. Will be broadened.
        realStats.Health -= damageSource.Attack;
        HealthValueChanged?.Invoke(realStats.HealthPct);
    }


}
