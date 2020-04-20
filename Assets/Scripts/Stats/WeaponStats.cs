using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : Stats
{
    public WeaponStats(WeaponStatsAsset startStats) : base(startStats)
    {
    }

    public override int[] AllStats() => new int[] { Attack, Holy, Dark, Fire, Ice, Arcane, Nature, Lightning, Earth, Slice, Smash};
}
