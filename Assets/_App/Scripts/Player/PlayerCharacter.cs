using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacter
{
    [SerializeField]
    private string playerName, characerName;

    [SerializeField]
    private Levels levels = new();

    [SerializeField]
    private Stats stats = new();

    [SerializeField]
    Equipment[] equipment = new Equipment[Enum.GetValues(typeof(EquipmentSlot)).Length];

    [SerializeField]
    Inventory inventory = new ();

    public string PlayerName { get => playerName; set => playerName = value; }
    
    public string CharacterName { get => characerName; set => characerName = value; }

    public Levels Levels { get => levels; set => levels = value; }

    public Stats Stats { get => stats; set => stats = value; }
    
    public Equipment[] Equipment { get => equipment; set => equipment = value; }

    public Inventory Inventory { get => inventory; set => inventory = value; }

    public PlayerCharacter()
    {
        stats.Health.MaxValue = 100;
        stats.MeleeAttack.MaxValue = 1;
        stats.MeleeDefense.MaxValue = 1;
        stats.RangeAttack.MaxValue = 1;
        stats.RangeDefense.MaxValue = 1;
        stats.MagicAttack.MaxValue = 1;
        stats.MagicDefense.MaxValue = 1;
    }
}
