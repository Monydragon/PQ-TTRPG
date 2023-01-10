using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat meleeAttack, meleeDefense, rangeAttack, rangeDefense, magicAttack, magicDefense, critChance;

    public Stat Health { get => health; set => health = value; }

    public Stat MeleeAttack { get => meleeAttack; set => meleeAttack = value; }
    public Stat MeleeDefense { get => meleeDefense; set => meleeDefense = value; }
    public Stat RangeAttack { get => rangeAttack; set => rangeAttack = value; }
    public Stat RangeDefense { get => rangeDefense; set => rangeDefense = value; }
    public Stat MagicAttack { get => magicAttack; set => magicAttack = value; }
    public Stat MagicDefense { get => magicDefense; set => magicDefense = value; }
    public Stat CritChance { get => critChance; set => critChance = value; }

    public Stats()
    {
        health = new();
        meleeAttack = new();
        meleeDefense = new();
        rangeAttack = new();
        rangeDefense = new();
        magicAttack = new();
        magicDefense = new();
        critChance = new();
    }
}
