using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levels
{
    [SerializeField]
    private LevelHandler melee, range, magic;

    public int CombatLevel => (int)(melee.Level + range.Level + magic.Level) / 3;
    public LevelHandler Melee { get => melee; set => melee = value; }
    public LevelHandler Range { get => range; set => range = value; }
    public LevelHandler Magic { get => magic; set => magic = value; }
}
