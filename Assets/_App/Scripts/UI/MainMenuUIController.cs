using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;

public class MainMenuUIController : MonoBehaviour
{
    public static MainMenuUIController instance;

    [SerializeField]
    private GameObject loadCharacterButtonPrefab, loadCharactersContent, itemDatabaseButtonPrefab, loadItemDatabaseContent, modifyItemDatabaseContent, itemDatabaseItemButtonPrefab;

    [SerializeField]
    private TMP_InputField playerCharacterNameInput, playerNameInput, itemDatabaseFileNameInput;

    public GameObject LoadCharacterButtonPrefab { get => loadCharacterButtonPrefab; set => loadCharacterButtonPrefab = value; }
    public GameObject LoadCharactersContent { get => loadCharactersContent; set => loadCharactersContent = value; }
    public GameObject ItemDatabaseButtonPrefab { get => itemDatabaseButtonPrefab; set => itemDatabaseButtonPrefab = value; }
    public GameObject LoadItemDatabaseContent { get => loadItemDatabaseContent; set => loadItemDatabaseContent = value; }
    public GameObject ModifyItemDatabaseContent { get => modifyItemDatabaseContent; set => modifyItemDatabaseContent = value; }
    public GameObject ItemDatabaseItemButtonPrefab { get => itemDatabaseItemButtonPrefab; set => itemDatabaseItemButtonPrefab = value; }
    public TMP_InputField PlayerCharacterNameInput { get => playerCharacterNameInput; set => playerCharacterNameInput = value; }
    public TMP_InputField PlayerNameInput { get => playerNameInput; set => playerNameInput = value; }
    public TMP_InputField ItemDatabaseFileNameInput { get => itemDatabaseFileNameInput; set => itemDatabaseFileNameInput = value; }

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
    }
    public void ClearLoadCharacterContent()
    {
        if (loadCharactersContent.transform.childCount <= 0) return;

        for (int i = 0; i < loadCharactersContent.transform.childCount; i++)
        {
            Destroy(loadCharactersContent.transform.GetChild(i).gameObject);
        }
    }

    public void ClearLoadItemDatabaseContent()
    {
        if (loadItemDatabaseContent.transform.childCount <= 0) return;

        for (int i = 0; i < loadItemDatabaseContent.transform.childCount; i++)
        {
            Destroy(loadItemDatabaseContent.transform.GetChild(i).gameObject);
        }
    }

    public void ClearModifyItemDatabaseContent()
    {
        if (modifyItemDatabaseContent.transform.childCount <= 0) return;
        for (int i = 0; i < modifyItemDatabaseContent.transform.childCount; i++)
        {
            Destroy(modifyItemDatabaseContent.transform.GetChild(i).gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGamePlayer()
    {
        if(playerCharacterNameInput != null)
        {
            if (!string.IsNullOrWhiteSpace(playerCharacterNameInput.text) && !string.IsNullOrWhiteSpace(playerNameInput.text))
            {
                PlayerManager.instance.playerCharacter.PlayerName = playerNameInput.text;
                PlayerManager.instance.playerCharacter.CharacterName = playerCharacterNameInput.text;
                SceneManager.LoadScene("PlayerGame");
            }
        }
    }

    public void LoadCharacters()
    {
        ClearLoadCharacterContent();
        string[] filePaths = Directory.GetFiles($"{Application.persistentDataPath}", "*.save");
        for (int i = 0; i < filePaths.Length; i++)
        {
            string path = filePaths[i];
            var filename = Path.GetFileName(path);
            var json = File.ReadAllText(path);
            var character = JsonConvert.DeserializeObject<PlayerCharacter>(json);
            var button = Instantiate(loadCharacterButtonPrefab, loadCharactersContent.transform);
            var loadButton = button.GetComponent<CharacterLoadButton>();
            loadButton.FileName = filename;
            loadButton.Character = character;
        }

    }

    public void LoadItemDatabaseFiles()
    {
        ClearLoadItemDatabaseContent();
        string[] filePaths = Directory.GetFiles($"{Application.persistentDataPath}", "*.json");
        for (int i = 0; i < filePaths.Length; i++)
        {
            string path = filePaths[i];
            var filename = Path.GetFileName(path);
            var button = Instantiate(itemDatabaseButtonPrefab, loadItemDatabaseContent.transform);
            var loadButton = button.GetComponent<ItemDatabaseLoadButton>();
            loadButton.Database = ItemDatabase.instance;
            loadButton.Database.Load(path, filename);
            loadButton.FileName = filename;
            loadButton.Count = loadButton.Database.Items.Count;
        }
    }

    public void SaveItemDatabase()
    {
        if(ItemDatabase.instance != null && !string.IsNullOrWhiteSpace(itemDatabaseFileNameInput.text))
        {
            PopupDisplayUI.instance.ShowPopup($"Save Item Database: {ItemDatabaseFileNameInput.text}.json", PopupDisplayUI.PopupPosition.Middle, ()=>
            {
                ItemDatabase.instance.SaveFileName = ItemDatabaseFileNameInput.text + ".json";
                ItemDatabase.instance.Save();
            });

        }
    }

    public void ModifyItemDatabase()
    {
        ClearModifyItemDatabaseContent();
        for (int i = 0; i < ItemDatabase.instance.Items.Count; i++)
        {
            var btn = Instantiate(itemDatabaseItemButtonPrefab, modifyItemDatabaseContent.transform);
            var itemBtn = btn.GetComponent<ItemDatabaseUIButton>();
            itemBtn.Item = ItemDatabase.instance.Items[i];
        }
    }
}
