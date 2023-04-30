using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine.Serialization;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    [SerializeField] private List<ItemDatabaseContainer> itemDatabases;
    [SerializeField] private ItemDatabaseContainer serverDatabase = new();
    [SerializeField] private ItemDatabaseContainer currentDatabase = new ();
    
    public List<ItemDatabaseContainer> ItemDatabases { get => itemDatabases; set => itemDatabases = value;}
    public ItemDatabaseContainer ServerDatabase { get => serverDatabase; set => serverDatabase = value;}
    
    public ItemDatabaseContainer CurrentDatabase { get => currentDatabase; set => currentDatabase = value;}
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            try
            {
                var files = Directory.GetFiles(Application.persistentDataPath, "*.json");
                for (int i = 0; i < files.Length; i++)
                {
                    var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    var itemDb = JsonConvert.DeserializeObject<ItemDatabaseContainer>(File.ReadAllText(files[i]),jsonSettings);
                    itemDatabases.Add(itemDb);
                }

                currentDatabase = itemDatabases.OrderByDescending(x => x.LastUpdatedUtcTime).Select(x => x).FirstOrDefault();
                if (currentDatabase == null)
                {
                    if (serverDatabase != null)
                    {
                        LoadServerItems();
                    }
                    else
                    {
                        currentDatabase = new ItemDatabaseContainer();
                        Save();
                    }
                }
            }
            catch (Exception e)
            {
                PopupDisplayUI.instance.ShowPopup("Error Loading Database", PopupDisplayUI.PopupPosition.Middle, () =>
                {
                    Debug.Log(e.Message);
                });
            }


        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadServerItems()
    {
        currentDatabase = serverDatabase;
        currentDatabase.Items.Clear();
        var items = Resources.LoadAll("Items", typeof(Item)).Cast<Item>().ToArray();
        for (var i = 0; i < items.Length; i++)
        {
            var t = items[i];
            currentDatabase.Items.Add(t);
        }
        Save();

#if UNITY_EDITOR
        var path = Path.Combine("Assets", "_App", "Resources", "Items");
        var filename = currentDatabase.SaveFileName;
        File.WriteAllText(Path.Combine(path,filename),JsonConvert.SerializeObject(currentDatabase, Formatting.Indented));
        AssetDatabase.Refresh();
#endif
    }

    public bool Save(string path = "", string fileName = "")
    {
        if(currentDatabase.Items.Count <= 0) { return false; }

        try
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var json = JsonConvert.SerializeObject(currentDatabase, Formatting.Indented,jsonSettings);

            if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(fileName))
            {
                path = Path.Combine(Application.persistentDataPath, currentDatabase.SaveFileName);
            }
            else
            {
                path = Path.Combine(path, fileName);
            }

            File.WriteAllText(path, json);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    public bool Load(string path = "", string fileName = "")
    {
        if (currentDatabase != null)
        {
            currentDatabase.SaveFileName = fileName;
        }
        
        if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(fileName))
        {
            path = Path.Combine(Application.persistentDataPath, currentDatabase.SaveFileName);
        }
        else
        {
            path = Path.Combine(path, fileName);
        }

        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var json = File.ReadAllText(path);
            currentDatabase = JsonConvert.DeserializeObject<ItemDatabaseContainer>(json, jsonSettings);
            
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
    public void LoadFromServer(string url)
    {
        try
        {
            using var webClient = new WebClient();
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var json = webClient.DownloadString(url);
            currentDatabase = JsonConvert.DeserializeObject<ItemDatabaseContainer>(json, jsonSettings);
            PopupDisplayUI.instance.ShowPopup("Loaded Item Database from Server!", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        catch (WebException ex)
        {
            Debug.LogError("An error occurred while downloading the JSON file: " + ex.Message);
            PopupDisplayUI.instance.ShowPopup("Failed to Load Item Database from Server!", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
    }
    
}
