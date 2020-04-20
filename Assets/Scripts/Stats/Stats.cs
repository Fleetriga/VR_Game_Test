using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Stats
{
    [SerializeField] int attack;
    [SerializeField] int holy;
    [SerializeField] int dark;
    [SerializeField] int fire;
    [SerializeField] int ice;
    [SerializeField] int arcane;
    [SerializeField] int nature;
    [SerializeField] int lightning;
    [SerializeField] int earth;
    [SerializeField] int slice;
    [SerializeField] int smash;

    public int Attack { get => attack; set => attack = value; }
    public int Holy { get => holy; set => holy = value; }
    public int Dark { get => dark; set => dark = value; }
    public int Fire { get => fire; set => fire = value; }
    public int Ice { get => ice; set => ice = value; }
    public int Arcane { get => arcane; set => arcane = value; }
    public int Nature { get => nature; set => nature = value; }
    public int Lightning { get => lightning; set => lightning = value; }
    public int Earth { get => earth; set => earth = value; }
    public int Slice { get => slice; set => slice = value; }
    public int Smash { get => smash; set => smash = value; }

    public Stats(StatsAsset startingStats)
    {
        attack = startingStats.Attack;
        holy = startingStats.Holy;
        dark = startingStats.Dark;
        fire = startingStats.Fire;
        ice = startingStats.Ice;
        arcane = startingStats.Arcane;
        nature = startingStats.Nature;
        lightning = startingStats.Lightning;
        earth = startingStats.Earth;
        slice = startingStats.Slice;
        smash = startingStats.Smash;
    }

    public virtual int[] AllStats() => new int[] { Attack, Holy, Dark, Fire, Ice, Arcane, Nature, Lightning, Earth, Slice, Smash };
}

