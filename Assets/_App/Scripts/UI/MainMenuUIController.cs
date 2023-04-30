using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class MainMenuUIController : MonoBehaviour
{
    public static MainMenuUIController instance;

    [SerializeField]
    private GameObject loadCharacterButtonPrefab, loadCharactersContent, itemDatabaseButtonPrefab, loadItemDatabaseContent, modifyItemDatabaseContent, itemDatabaseItemButtonPrefab, addModifyItemDatabasePanel, modifyItemDatabasePanel;

    [SerializeField]
    private TMP_InputField playerCharacterNameInput, playerNameInput, itemDatabaseFileNameInput;
    
    [SerializeField]
    private TMP_InputField itemNameInput, itemDescriptionInput, itemCostInput, healthInput, meleeAttackInput, meleeDefenseInput, rangeAttackInput,rangeDefenseInput, magicAttackInput, magicDefenseInput, critChanceInput;

    [SerializeField] 
    private TMP_Dropdown itemTypeDropdown, equipmentSlotDropdown, equipmentTypeDropdown;

    [SerializeField] private Item currentItem;
    public GameObject LoadCharacterButtonPrefab { get => loadCharacterButtonPrefab; set => loadCharacterButtonPrefab = value; }
    public GameObject LoadCharactersContent { get => loadCharactersContent; set => loadCharactersContent = value; }
    public GameObject ItemDatabaseButtonPrefab { get => itemDatabaseButtonPrefab; set => itemDatabaseButtonPrefab = value; }
    public GameObject LoadItemDatabaseContent { get => loadItemDatabaseContent; set => loadItemDatabaseContent = value; }
    public GameObject ModifyItemDatabaseContent { get => modifyItemDatabaseContent; set => modifyItemDatabaseContent = value; }
    public GameObject ItemDatabaseItemButtonPrefab { get => itemDatabaseItemButtonPrefab; set => itemDatabaseItemButtonPrefab = value; }
    public GameObject AddModifyItemDatabasePanel { get => addModifyItemDatabasePanel; set => addModifyItemDatabasePanel = value; }
    public GameObject ModifyItemDatabasePanel { get => modifyItemDatabasePanel; set => modifyItemDatabasePanel = value; }
    public TMP_InputField PlayerCharacterNameInput { get => playerCharacterNameInput; set => playerCharacterNameInput = value; }
    public TMP_InputField PlayerNameInput { get => playerNameInput; set => playerNameInput = value; }
    public TMP_InputField ItemDatabaseFileNameInput { get => itemDatabaseFileNameInput; set => itemDatabaseFileNameInput = value; }
    public TMP_InputField ItemNameInput { get => itemNameInput; set => itemNameInput = value; }
    public TMP_InputField ItemDescriptionInput { get => itemDescriptionInput; set => itemDescriptionInput = value; }
    public TMP_InputField ItemCostInput { get => itemCostInput; set => itemCostInput = value; }
    public TMP_InputField HealthInput { get => healthInput; set => healthInput = value; }
    public TMP_InputField MeleeAttackInput { get => meleeAttackInput; set => meleeAttackInput = value; }
    public TMP_InputField MeleeDefenseInput { get => meleeDefenseInput; set => meleeDefenseInput = value; }
    public TMP_InputField RangeAttackInput { get => rangeAttackInput; set => rangeAttackInput = value; }
    public TMP_InputField RangeDefenseInput { get => rangeDefenseInput; set => rangeDefenseInput = value; }
    public TMP_InputField MagicAttackInput { get => magicAttackInput; set => magicAttackInput = value; }
    public TMP_InputField MagicDefenseInput { get => magicDefenseInput; set => magicDefenseInput = value; }
    public TMP_InputField CritChanceInput { get => critChanceInput; set => critChanceInput = value; }
    public TMP_Dropdown ItemTypeDropdown { get => itemTypeDropdown; set => itemTypeDropdown = value; }
    public TMP_Dropdown EquipmentSlotDropdown { get => equipmentSlotDropdown; set => equipmentSlotDropdown = value; }
    public TMP_Dropdown EquipmentTypeDropdown { get => equipmentTypeDropdown; set => equipmentTypeDropdown = value; }
    public Item CurrentItem { get => currentItem; set => currentItem = value; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            equipmentSlotDropdown.AddOptions(Enum.GetNames(typeof(EquipmentSlot)).ToList());
            equipmentTypeDropdown.AddOptions(Enum.GetNames(typeof(EquipmentType)).ToList());
        }
    }

    private void OnEnable()
    {
        ItemDatabaseUIButton.OnItemEdit += OnItemEdit;
    }

    private void OnItemEdit(Item item)
    {
        if (item != null)
        {
            currentItem = item;
            addModifyItemDatabasePanel.SetActive(true);
            modifyItemDatabasePanel.SetActive(false);
            itemNameInput.text = item.ItemName;
            itemDescriptionInput.text = item.Description;
            itemCostInput.text = $"Cost: {item.Cost}";
            if (item is Equipment equipment)
            {
                itemTypeDropdown.value = 1;
                equipmentSlotDropdown.gameObject.SetActive(true);
                healthInput.gameObject.SetActive(true);
                meleeAttackInput.gameObject.SetActive(true);
                meleeDefenseInput.gameObject.SetActive(true);
                rangeAttackInput.gameObject.SetActive(true);
                rangeDefenseInput.gameObject.SetActive(true);
                magicAttackInput.gameObject.SetActive(true);
                magicDefenseInput.gameObject.SetActive(true);
                critChanceInput.gameObject.SetActive(true);
                healthInput.text = $"Health: {equipment.Stats.Health.MaxValue}";
                meleeAttackInput.text = $"Melee Attack: {equipment.Stats.MeleeAttack.MaxValue}";
                meleeDefenseInput.text = $"Melee Defense: {equipment.Stats.MeleeDefense.MaxValue}";
                rangeAttackInput.text = $"Range Attack: {equipment.Stats.RangeAttack.MaxValue}";
                rangeDefenseInput.text = $"Range Defense: {equipment.Stats.RangeDefense.MaxValue}";
                magicAttackInput.text = $"Magic Attack: {equipment.Stats.MagicAttack.MaxValue}";
                magicDefenseInput.text = $"Health: {equipment.Stats.MagicDefense.MaxValue}";
                critChanceInput.text = $"Crit Chance: {equipment.Stats.CritChance.MaxValue}";
            }
            else
            {
                itemTypeDropdown.value = 0;
                
            }
            
            
        }
    }

    public void ItemTypeChanged()
    {
        if (itemTypeDropdown.value == 0)
        {
            equipmentSlotDropdown.gameObject.SetActive(false);
            equipmentTypeDropdown.gameObject.SetActive(false);
            healthInput.gameObject.SetActive(false);
            meleeAttackInput.gameObject.SetActive(false);
            meleeDefenseInput.gameObject.SetActive(false);
            rangeAttackInput.gameObject.SetActive(false);
            rangeDefenseInput.gameObject.SetActive(false);
            magicAttackInput.gameObject.SetActive(false);
            magicDefenseInput.gameObject.SetActive(false);
            critChanceInput.gameObject.SetActive(false);
        }

        if (itemTypeDropdown.value == 1)
        {
            equipmentSlotDropdown.gameObject.SetActive(true);
            equipmentTypeDropdown.gameObject.SetActive(true);
            healthInput.gameObject.SetActive(true);
            meleeAttackInput.gameObject.SetActive(true);
            meleeDefenseInput.gameObject.SetActive(true);
            rangeAttackInput.gameObject.SetActive(true);
            rangeDefenseInput.gameObject.SetActive(true);
            magicAttackInput.gameObject.SetActive(true);
            magicDefenseInput.gameObject.SetActive(true);
            critChanceInput.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        ItemDatabaseUIButton.OnItemEdit -= OnItemEdit;
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
        try
        {
            string[] filePaths = Directory.GetFiles($"{Application.persistentDataPath}", "*.json");
            for (int i = 0; i < filePaths.Length; i++)
            {
                var button = Instantiate(itemDatabaseButtonPrefab, loadItemDatabaseContent.transform);
                var loadButton = button.GetComponent<ItemDatabaseLoadButton>();
                var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                var json = File.ReadAllText(filePaths[i]);
                loadButton.DatabaseContainer = JsonConvert.DeserializeObject<ItemDatabaseContainer>(json, jsonSettings);
            }
        }
        catch (Exception e)
        {
            PopupDisplayUI.instance.ShowPopup("Failed to load database files", PopupDisplayUI.PopupPosition.Middle,
                () =>
                {
                    Debug.Log(e.Message);
                });
        }

    }

    public void SaveItemDatabase()
    {
        if(ItemDatabase.instance != null && !string.IsNullOrWhiteSpace(itemDatabaseFileNameInput.text))
        {
            PopupDisplayUI.instance.ShowPopup($"Save Item Database: {ItemDatabaseFileNameInput.text}.json", PopupDisplayUI.PopupPosition.Middle, ()=>
            {
                ItemDatabase.instance.CurrentDatabase.SaveFileName = ItemDatabaseFileNameInput.text + ".json";
                ItemDatabase.instance.Save();
            });

        }
    }

    public void SetSaveFilename()
    {
        if (ItemDatabase.instance.CurrentDatabase != null)
        {
            var splitFilename = ItemDatabase.instance.CurrentDatabase?.SaveFileName.Split('.');
            itemDatabaseFileNameInput.text = splitFilename[0];
        }
    }

    public void ModifyItemDatabase()
    {
        ClearModifyItemDatabaseContent();
        for (int i = 0; i < ItemDatabase.instance.CurrentDatabase?.Items.Count; i++)
        {
            var btn = Instantiate(itemDatabaseItemButtonPrefab, modifyItemDatabaseContent.transform);
            var itemBtn = btn.GetComponent<ItemDatabaseUIButton>();
            itemBtn.Item = ItemDatabase.instance.CurrentDatabase.Items[i];
        }
    }

    public void SaveItemToDatabase()
    {
        currentItem = ItemDatabase.instance.CurrentDatabase?.Items.Find(x=> x.ItemName.Equals(itemNameInput.text));
        Equipment equip = null;
        if (currentItem != null)
        {
            equip = currentItem as Equipment;
        }
        if (string.IsNullOrWhiteSpace(itemNameInput.text))
        {
            PopupDisplayUI.instance.ShowPopup("Please enter a vaild item name", PopupDisplayUI.PopupPosition.Middle,
                () =>
                {
                    
                });
            return;
        }
        Regex re = new Regex(@"\d+");
        if (itemTypeDropdown.value == 0)
        {
            if (currentItem == null)
            {
                currentItem = ScriptableObject.CreateInstance<Item>();
            }
            currentItem.ItemName = itemNameInput.text;
            currentItem.Description = itemDescriptionInput.text;
            int.TryParse(re.Match(itemCostInput.text).Value, out int cost);
            currentItem.Cost = cost;
        }

        if (itemTypeDropdown.value == 1)
        {
            if (currentItem == null)
            {
                currentItem = ScriptableObject.CreateInstance<Equipment>();
            }
            equip = currentItem as Equipment;
            equip.ItemName = itemNameInput.text;
            equip.Description = itemDescriptionInput.text;
            int.TryParse(re.Match(itemCostInput.text).Value, out int cost);
            equip.Cost = cost;
            equip.Slot = (EquipmentSlot)equipmentSlotDropdown.value;
            equip.Type = (EquipmentType)equipmentTypeDropdown.value;
            
            int.TryParse(re.Match(healthInput.text).Value, out int health);
            int.TryParse(re.Match(meleeAttackInput.text).Value, out int meleeAttack);
            int.TryParse(re.Match(meleeDefenseInput.text).Value, out int meleeDefense);
            int.TryParse(re.Match(rangeAttackInput.text).Value, out int rangeAttack);
            int.TryParse(re.Match(rangeDefenseInput.text).Value, out int rangeDefense);
            int.TryParse(re.Match(magicAttackInput.text).Value, out int magicAttack);
            int.TryParse(re.Match(magicDefenseInput.text).Value, out int magicDefense);
            int.TryParse(re.Match(critChanceInput.text).Value, out int critChance);
            equip.Stats.Health.MaxValue = health;
            equip.Stats.MeleeAttack.MaxValue = meleeAttack;
            equip.Stats.MeleeDefense.MaxValue = meleeDefense;
            equip.Stats.RangeAttack.MaxValue = rangeAttack;
            equip.Stats.RangeDefense.MaxValue = rangeDefense;
            equip.Stats.MagicAttack.MaxValue = magicAttack;
            equip.Stats.MagicDefense.MaxValue = magicDefense;
            equip.Stats.CritChance.MaxValue = critChance;
        }

        var foundItem = ItemDatabase.instance.CurrentDatabase?.Items.Find(x => x.ItemName.Equals(currentItem.ItemName));
        if(foundItem != null)
        {
            foundItem.ItemName = currentItem.ItemName;
            foundItem.Description = currentItem.Description;
            foundItem.Cost = currentItem.Cost;
            if (foundItem is Equipment foundEquipment && equip is Equipment currentEquipment)
            {
                foundEquipment.Slot = currentEquipment.Slot;
                foundEquipment.Stats = currentEquipment.Stats;
            }
            ItemDatabase.instance.CurrentDatabase.LastUpdatedUtcTime = DateTime.UtcNow.ToFileTimeUtc();
        }
        else
        {
            ItemDatabase.instance.CurrentDatabase?.Items.Add(currentItem);
            ItemDatabase.instance.CurrentDatabase.LastUpdatedUtcTime = DateTime.UtcNow.ToFileTimeUtc();
        }
    }

    public void ClearNewItemAdd()
    {
        itemNameInput.text = string.Empty;
        itemDescriptionInput.text = string.Empty;
        itemCostInput.text = string.Empty;
        itemTypeDropdown.value = 0;
        equipmentSlotDropdown.value = 0;
        equipmentTypeDropdown.value = 0;
        healthInput.text = string.Empty;
        meleeAttackInput.text = string.Empty;
        meleeDefenseInput.text = string.Empty;
        rangeAttackInput.text = string.Empty;
        rangeDefenseInput.text = string.Empty;
        magicAttackInput.text = string.Empty;
        magicDefenseInput.text = string.Empty;
        critChanceInput.text = string.Empty;
    }
}
