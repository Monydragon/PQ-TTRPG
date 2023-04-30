using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabaseContainer
{
    [SerializeField]
    private string saveFileName = "ItemDb.json";
    [SerializeField]
    private List<Item> items = new ();

    [SerializeField] 
    private long lastUpdatedUtcTime = DateTime.UtcNow.ToFileTimeUtc();
    
    public string SaveFileName { get => saveFileName; set => saveFileName = value; }
    public List<Item> Items { get => items; set => items = value; }
    public long LastUpdatedUtcTime { get => lastUpdatedUtcTime; set => lastUpdatedUtcTime = value; }
}