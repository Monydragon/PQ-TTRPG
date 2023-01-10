using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    private List<InventorySlot> items = new();

    public List<InventorySlot> Items { get => items; set => items = value; }

    public bool HasItems { get => items.Count > 0; }

    public Item GetItem(string name)
    {
        return items.Find(x => x.Item.ItemName.ToLower() == name.ToLower()).Item;
    }

    public InventorySlot GetItemSlot(string name)
    {
        return items.Find(x => x.Item.ItemName.ToLower() == name.ToLower());
    }

    public bool AddItem(Item item, int amount = 1)
    {
        if (item == null)
            return false;
        var exitingItem = items.Find(x => x.Item.ItemName.ToLower() == item.ItemName.ToLower());
        if (exitingItem != null)
        {
            exitingItem.Amount += amount;
        }
        else
        {
            items.Add(new InventorySlot { Item = item, Amount = amount });
        }
        return true;
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        var existingItem = items.Find(x => x.Item.ItemName.ToLower() == item.ItemName.ToLower());
        if (existingItem == null)
            return false;
        if(existingItem.Amount - amount > 0)
        {
            existingItem.Amount -= amount;
        }
        else
        {
            items.Remove(existingItem);
        }

        return true;
    }
}
