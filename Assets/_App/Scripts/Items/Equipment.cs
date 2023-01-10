using UnityEngine;

[CreateAssetMenu(fileName ="Equipment", menuName ="DLS/Equipment")]
public class Equipment : Item
{
    [SerializeField]
    private Stats stats;

    [SerializeField]
    EquipmentSlot slot;

    [SerializeField]
    EquipmentType type;

    public Stats Stats { get => stats; set => stats = value; }
    public EquipmentSlot Slot { get => slot; set => slot = value; }
    public EquipmentType Type { get => type; set => type = value; }

    public override void Use(PlayerCharacter character)
    {
        base.Use(character);
        var currentEquip = character.Equipment[(int)slot];
        if(currentEquip != null)
        {
            character.Stats.Health.MaxValue -= currentEquip.stats.Health.MaxValue;
            character.Stats.MeleeAttack.MaxValue -= currentEquip.stats.MeleeAttack.MaxValue;
            character.Stats.MeleeDefense.MaxValue -= currentEquip.stats.MeleeDefense.MaxValue;
            character.Stats.RangeAttack.MaxValue -= currentEquip.stats.RangeAttack.MaxValue;
            character.Stats.RangeDefense.MaxValue -= currentEquip.stats.RangeDefense.MaxValue;
            character.Stats.MagicAttack.MaxValue -= currentEquip.stats.MagicAttack.MaxValue;
            character.Stats.MagicDefense.MaxValue -= currentEquip.stats.MagicDefense.MaxValue;
            character.Stats.CritChance.MaxValue -= currentEquip.stats.CritChance.MaxValue;
            character.Inventory.AddItem(currentEquip);
            //Debug.Log($"{character.CharacterName} Unequipped {currentEquip.itemName}");
            character.Equipment[(int)slot] = null;
        }
        else
        {
            //Debug.Log($"{character.CharacterName} Equipped {itemName}");
            character.Equipment[(int)slot] = this;
            character.Stats.Health.MaxValue += stats.Health.MaxValue;
            character.Stats.MeleeAttack.MaxValue += stats.MeleeAttack.MaxValue;
            character.Stats.MeleeDefense.MaxValue += stats.MeleeDefense.MaxValue;
            character.Stats.RangeAttack.MaxValue += stats.RangeAttack.MaxValue;
            character.Stats.RangeDefense.MaxValue += stats.RangeDefense.MaxValue;
            character.Stats.MagicAttack.MaxValue += stats.MagicAttack.MaxValue;
            character.Stats.MagicDefense.MaxValue += stats.MagicDefense.MaxValue;
            character.Stats.CritChance.MaxValue += stats.CritChance.MaxValue;
            character.Inventory.RemoveItem(this);
        }

        
    }

}
