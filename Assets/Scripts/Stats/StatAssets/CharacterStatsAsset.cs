using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats")]
public class CharacterStatsAsset : StatsAsset
{
    [SerializeField] protected int health;
    [SerializeField] protected int defense;

    public int Health { get => health; }
    public int Defense { get => defense; }
}
