using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemDatabaseUIButton : MonoBehaviour
{

    public static Action<Item> OnItemEdit;
    
    [SerializeField]
    private TMP_Text itemNameText, itemDescriptionText,equipmentSlotsText,equipmentTypeText,healthText,meleeAttackText,meleeDefenseText,rangeAttackText,rangeDefenseText,magicAttackText,magicDefenseText,critChanceText;

    [SerializeField]
    private TMP_Text itemCostText;

    [SerializeField] private Item item;

    public TMP_Text ItemNameText
    {
        get => itemNameText;
        set => itemNameText = value;
    }

    public TMP_Text ItemDescriptionText
    {
        get => itemDescriptionText;
        set => itemDescriptionText = value;
    }

    public TMP_Text EquipmentSlotsText
    {
        get => equipmentSlotsText;
        set => equipmentSlotsText = value;
    }
    
    public TMP_Text EquipmentTypeText
    {
        get => equipmentTypeText;
        set => equipmentTypeText = value;
    }

    public TMP_Text HealthText
    {
        get => healthText;
        set => healthText = value;
    }

    public TMP_Text MeleeAttackText
    {
        get => meleeAttackText;
        set => meleeAttackText = value;
    }

    public TMP_Text MeleeDefenseText
    {
        get => meleeDefenseText;
        set => meleeDefenseText = value;
    }

    public TMP_Text RangeAttackText
    {
        get => rangeAttackText;
        set => rangeAttackText = value;
    }

    public TMP_Text RangeDefenseText
    {
        get => rangeDefenseText;
        set => rangeDefenseText = value;
    }

    public TMP_Text MagicAttackText
    {
        get => magicAttackText;
        set => magicAttackText = value;
    }

    public TMP_Text MagicDefenseText
    {
        get => magicDefenseText;
        set => magicDefenseText = value;
    }

    public TMP_Text CritChanceText
    {
        get => critChanceText;
        set => critChanceText = value;
    }
    
    public TMP_Text ItemCostText
    {
        get => itemCostText;
        set => itemCostText = value;
    }
    
    public Item Item
    {
        get => item;
        set => item = value;
    }

    private void Update()
    {
        if (item != null)
        {
            var equipment = item as Equipment;
            itemNameText.text = item.ItemName;
            itemDescriptionText.text = item.Description;
            itemCostText.text = $"Cost: {item.Cost}";
            if (equipment != null)
            {
                equipmentSlotsText.text = $"Slot: {equipment.Slot.ToString()}";
                equipmentTypeText.text = $"Type: {equipment.Type.ToString()}";
                healthText.text = $"Health: {equipment.Stats.Health.MaxValue}";
                meleeAttackText.text = $"Melee Attack: {equipment.Stats.MeleeAttack.MaxValue}";
                meleeDefenseText.text = $"Melee Defense: {equipment.Stats.MeleeDefense.MaxValue}";
                rangeAttackText.text = $"Range Attack: {equipment.Stats.RangeAttack.MaxValue}";
                rangeDefenseText.text = $"Range Defense: {equipment.Stats.RangeDefense.MaxValue}";
                magicAttackText.text = $"Magic Attack: {equipment.Stats.MagicAttack.MaxValue}";
                magicDefenseText.text = $"Magic Defense: {equipment.Stats.MagicDefense.MaxValue}";
                critChanceText.text = $"Crit Chance: {equipment.Stats.CritChance.MaxValue}";
            }
            else
            {
                equipmentSlotsText.gameObject.SetActive(false);
                equipmentTypeText.gameObject.SetActive(false);
                healthText.gameObject.SetActive(false);
                meleeAttackText.gameObject.SetActive(false);
                meleeDefenseText.gameObject.SetActive(false);
                rangeAttackText.gameObject.SetActive(false);
                rangeDefenseText.gameObject.SetActive(false);
                magicAttackText.gameObject.SetActive(false);
                magicDefenseText.gameObject.SetActive(false);
                critChanceText.gameObject.SetActive(false);
                
            }
            
        }
    }

    public void DeleteItem()
    {
        if (item != null)
        {
            PopupDisplayUI.instance.ShowPopup($"Confirm Delete Item: {item.ItemName}",PopupDisplayUI.PopupPosition.Middle,
                () =>
                {
                    ItemDatabase.instance.CurrentDatabase.Items.Remove(item);
                    Destroy(gameObject);
                }, () =>
                {
                    
                } );
        }
    }

    public void EditItem()
    {
        if (item != null)
        {
            OnItemEdit?.Invoke(item);
        }
    }
}
