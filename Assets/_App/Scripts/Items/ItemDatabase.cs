using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    [SerializeField]
    private string saveFileName = "ItemDatabase.json";
    [SerializeField]
    private List<Item> items = new ();

    public string SaveFileName { get => saveFileName; set => saveFileName = value; }
    public List<Item> Items { get => items; set => items = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool Save(string path = "", string fileName = "")
    {
        if(items.Count <= 0) { return false; }

        try
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var json = JsonConvert.SerializeObject(items, Formatting.Indented,jsonSettings);

            if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(fileName))
            {
                path = Path.Combine(Application.persistentDataPath, saveFileName);
            }
            else
            {
                path = Path.Combine(path, fileName);
            }
            
            // #if UNITY_ANDROID
            // path = "file://" + path;
            // #endif

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
        if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(fileName))
        {
            path = Path.Combine(Application.persistentDataPath, saveFileName);
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
            // #if UNITY_ANDROID
            // path = "file://" + path;
            // #endif
            var json = File.ReadAllText(path);
            items = JsonConvert.DeserializeObject<List<Item>>(json, jsonSettings);
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
            items = JsonConvert.DeserializeObject<List<Item>>(json, jsonSettings);
            PopupDisplayUI.instance.ShowPopup("Loaded Item Database from Server!", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        catch (WebException ex)
        {
            Debug.LogError("An error occurred while downloading the JSON file: " + ex.Message);
            PopupDisplayUI.instance.ShowPopup("Failed to Load Item Database from Server!", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
    }
    
}
