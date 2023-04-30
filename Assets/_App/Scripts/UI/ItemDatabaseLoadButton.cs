using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemDatabaseLoadButton : MonoBehaviour
{
    [SerializeField]
    private ItemDatabaseContainer databaseContainer;

    [SerializeField] 
    private TMP_Text fileNameText, countText;
    
    public ItemDatabaseContainer DatabaseContainer { get => databaseContainer; set => databaseContainer = value;}    
    public TMP_Text FileNameText { get => fileNameText; set => fileNameText = value; }
    public TMP_Text CountText { get => countText; set => countText = value; }

    // Update is called once per frame
    private void Update()
    {
        if (databaseContainer != null)
        {
            fileNameText.text = $"Filename: {databaseContainer.SaveFileName}";
            countText.text = $"Database Items Count: {databaseContainer.Items.Count}";
        }
    }

    public void LoadDatabase()
    {
        if(databaseContainer != null)
        {
            PopupDisplayUI.instance.ShowPopup($"Loaded Database: {databaseContainer.SaveFileName}", PopupDisplayUI.PopupPosition.Middle, 
                () => 
                {
                    ItemDatabase.instance.Load(Application.persistentDataPath, databaseContainer.SaveFileName); 
                });
        }
    }

    public void DeleteDatabase()
    {
        if(databaseContainer != null)
        {
            try
            {
                PopupDisplayUI.instance.ShowPopup($"Confirm Delete Database: {databaseContainer.SaveFileName}?", PopupDisplayUI.PopupPosition.Middle,
                    () => {
                        if (!string.IsNullOrWhiteSpace(databaseContainer.SaveFileName))
                        {
                            var fullPath = Path.Combine(Application.persistentDataPath, databaseContainer.SaveFileName);
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
                Debug.LogError($"Failed To delete {databaseContainer.SaveFileName}\n {e.Message}");
            }
        }
    }
}
