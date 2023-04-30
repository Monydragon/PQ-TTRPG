using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterItemButton : MonoBehaviour
{
    [SerializeField] TMP_Text itemNameText, itemDescriptionText, itemAmountText, itemHealthText, itemMeleeAttackText, itemMeleeDefenseText, itemRangeAttackText, itemRangeDefenseText, itemMagicAttackText, itemMagicDefenseText, itemCritChanceText, itemEquipmentSlotText, itemEquipmentTypeText;
    [SerializeField] bool isSelectEquipment;
    [SerializeField] InventorySlot selectedItemSlot;
    
    public TMP_Text ItemNameText { get => itemNameText; set => itemNameText = value; }
    public TMP_Text ItemDescriptionText { get => itemDescriptionText; set => itemDescriptionText = value; }
    public TMP_Text ItemAmountText { get => itemAmountText; set => itemAmountText = value; }
    public TMP_Text ItemHealthText { get => itemHealthText; set => itemHealthText = value; }
    public TMP_Text ItemMeleeAttackText { get => itemMeleeAttackText; set => itemMeleeAttackText = value; }
    public TMP_Text ItemMeleeDefenseText { get => itemMeleeDefenseText; set => itemMeleeDefenseText = value; }
    public TMP_Text ItemRangeAttackText { get => itemRangeAttackText; set => itemRangeAttackText = value; }
    public TMP_Text ItemRangeDefenseText { get => itemRangeDefenseText; set => itemRangeDefenseText = value; }
    public TMP_Text ItemMagicAttackText { get => itemMagicAttackText; set => itemMagicAttackText = value; }
    public TMP_Text ItemMagicDefenseText { get => itemMagicDefenseText; set => itemMagicDefenseText = value; }
    public TMP_Text ItemCritChanceText { get => itemCritChanceText; set => itemCritChanceText = value; }
    public TMP_Text ItemEquipmentSlotText { get => itemEquipmentSlotText; set => itemEquipmentSlotText = value; }
    public TMP_Text ItemEquipmentTypeText { get => itemEquipmentTypeText; set => itemEquipmentTypeText = value; }
    public bool IsSelectEquipment { get => isSelectEquipment; set => isSelectEquipment = value; }
    public InventorySlot SelectedItemSlot { get => selectedItemSlot; set => selectedItemSlot = value; }

    private void Update()
    {
        if (selectedItemSlot != null)
        {
            itemNameText.text = selectedItemSlot.Item.ItemName;
            itemDescriptionText.text = selectedItemSlot.Item.Description;
            ItemAmountText.text = $"Amount: {selectedItemSlot.Amount}";
            if(selectedItemSlot.Item is Equipment equipment)
            {
                // var equipment = (Equipment)selectedItemSlot.Item;
                itemEquipmentSlotText.text = $"Slot: {equipment.Slot}";
                itemEquipmentTypeText.text = $"Type: {equipment.Type}";
                itemHealthText.text = $"Health: {equipment.Stats.Health.MaxValue}";
                ItemMeleeAttackText.text = $"Melee Attack: {equipment.Stats.MeleeAttack.MaxValue}";
                itemMeleeDefenseText.text = $"Melee Defense: {equipment.Stats.MeleeDefense.MaxValue}";
                ItemRangeAttackText.text = $"Range Attack: {equipment.Stats.RangeAttack.MaxValue}";
                itemRangeDefenseText.text = $"Range Defense: {equipment.Stats.RangeDefense.MaxValue}";
                ItemMagicAttackText.text = $"Magic Attack: {equipment.Stats.MagicAttack.MaxValue}";
                itemMagicDefenseText.text = $"Melee Defense: {equipment.Stats.MagicDefense.MaxValue}";
                ItemCritChanceText.text = $"Crit Chance: {equipment.Stats.CritChance.MaxValue}";
            }
        }
    }
    
    public void AddItem()
    {
            PopupDisplayUI.instance.ShowPopup($"Added {selectedItemSlot.Item.ItemName}", PopupDisplayUI.PopupPosition.Middle,
                () =>
                {
                    PlayerManager.instance.playerCharacter.Inventory.AddItem(selectedItemSlot.Item, 1);
                });
    }

    public void RemoveItem()
    {
            PopupDisplayUI.instance.ShowPopup($"Removed {selectedItemSlot.Item.ItemName}", PopupDisplayUI.PopupPosition.Middle,
                () =>
                {
                    PlayerManager.instance.playerCharacter.Inventory.RemoveItem(selectedItemSlot.Item, 1);
                });
    }

    public void UseItem()
    {

        Equipment equipment = null;
        if (selectedItemSlot.Item.GetType() == typeof(Equipment))
        {
            equipment = (Equipment)selectedItemSlot.Item;
            if (PlayerManager.instance.playerCharacter.Equipment[(int)equipment.Slot] != null)
            {
                PopupDisplayUI.instance.ShowPopup($"Unequipped {PlayerManager.instance.playerCharacter.Equipment[(int)equipment.Slot]?.ItemName}", PopupDisplayUI.PopupPosition.Middle,
                    () =>
                    {
                    });
            }
            else
            {
                PopupDisplayUI.instance.ShowPopup($"Equipped {selectedItemSlot.Item.ItemName}", PopupDisplayUI.PopupPosition.Middle,
                    () =>
                    {
                    });
            }
        }
        else
        {
            PopupDisplayUI.instance.ShowPopup($"Used {selectedItemSlot.Item.ItemName}", PopupDisplayUI.PopupPosition.Middle,
                () =>
                {

                });
        }
        
        selectedItemSlot.Item.Use(PlayerManager.instance.playerCharacter);
        if (isSelectEquipment && equipment != null)
        {
            PlayerCharacterMenuController.instance.SelectEquipment((int)equipment.Slot);
        }
        else
        {
            PlayerCharacterMenuController.instance.UseItem();
        }

    }
}
