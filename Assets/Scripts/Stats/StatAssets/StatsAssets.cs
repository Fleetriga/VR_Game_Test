using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatsAsset : ScriptableObject
{
    [SerializeField] protected int attack;
    [SerializeField] protected int holy;
    [SerializeField] protected int dark;
    [SerializeField] protected int fire;
    [SerializeField] protected int ice;
    [SerializeField] protected int arcane;
    [SerializeField] protected int nature;
    [SerializeField] protected int lightning;
    [SerializeField] protected int earth;
    [SerializeField] protected int slice;
    [SerializeField] protected int smash;


    public int Attack { get => attack; }
    public int Holy { get => holy; }
    public int Dark { get => dark; }
    public int Fire { get => fire; }
    public int Ice { get => ice; }
    public int Arcane { get => arcane; }
    public int Nature { get => nature; }
    public int Lightning { get => lightning; }
    public int Earth { get => earth; }
    public int Slice { get => slice; }
    public int Smash { get => smash; }
}