using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemDatabaseLoadButton : MonoBehaviour
{
    [SerializeField]
    private string fileName;
    [SerializeField]
    private int count;

    [SerializeField]
    private TMP_Text fileNameText, countText;

    [SerializeField]
    private ItemDatabase database;

    public string FileName { get => fileName; set => fileName = value; }
    public int Count { get => count; set => count = value; }

    public TMP_Text FileNameText { get => fileNameText; set => fileNameText = value; }
    public TMP_Text CountText { get => countText; set => countText = value; }
    public ItemDatabase Database { get => database; set => database = value; }

    // Update is called once per frame
    private void Update()
    {
        if(database != null)
        {
            fileNameText.text = $"Filename: {fileName}";
            countText.text = $"Database Items Count: {count}";
        }
    }

    public void LoadDatabase()
    {
        if(database != null)
        {
            PopupDisplayUI.instance.ShowPopup($"Loaded Database: {fileName}", PopupDisplayUI.PopupPosition.Middle, 
                () => 
                {
                    ItemDatabase.instance.Load(Application.persistentDataPath, fileName); 
                });
        }
    }

    public void DeleteDatabase()
    {
        if(database != null)
        {
            try
            {
                PopupDisplayUI.instance.ShowPopup($"Confirm Delete Database: {fileName}?", PopupDisplayUI.PopupPosition.Middle,
                    () => {
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
                            File.Delete(fullPath);
                            if (MainMenuUIController.instance != null)
                            {
                                MainMenuUIController.instance.LoadItemDatabaseFiles();
                            }
                        }
                    }, () => { });
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed To delete {fileName}\n {e.Message}");
            }
        }
    }
}
