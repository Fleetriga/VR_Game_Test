using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Stats
{
    [SerializeField] int health;
    [SerializeField] int defense;
    int maxHealth;

    public int Health { get => health; set => health = value; }
    public int Defense { get => defense; set => defense = value; }
    public int MaxHealth { get => maxHealth; }
    public float HealthPct { get => (float)health / (float)maxHealth; }


    public CharacterStats(CharacterStatsAsset charStats) : base(charStats)
    {
        health = charStats.Health;
        defense = charStats.Defense;
        maxHealth = health;
    }

    public override int[] AllStats() => new int[] { Attack, Holy, Dark, Fire, Ice, Arcane, Nature, Lightning, Earth, Slice, Smash, Health, Defense, MaxHealth };
}
