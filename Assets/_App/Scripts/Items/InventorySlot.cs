using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private int amount;

    public Item Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }
}
