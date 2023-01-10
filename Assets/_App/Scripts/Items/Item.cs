using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="DLS/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    protected string itemName, description;
    [SerializeField]
    protected int cost;

    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public int Cost { get => cost; set => cost = value; }

    public virtual void Use(PlayerCharacter character)
    {
        //Debug.Log($"{itemName} Used on {character.CharacterName}");
    }
}
