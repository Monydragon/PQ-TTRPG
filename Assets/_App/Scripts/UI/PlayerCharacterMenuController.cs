using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerCharacterMenuController : MonoBehaviour
{
    public static PlayerCharacterMenuController instance;

    [SerializeField]
    private GameObject addItemButtonPrefab, removeItemButtonPrefab, useItemButtonPrefab;

    [SerializeField]
    private GameObject loadCharacterButtonPrefab;

    [SerializeField]
    private GameObject characterItemsContent;

    [SerializeField]
    private GameObject loadCharactersContent, characterNoteContent, characterNotePrefab;

    [SerializeField]
    private TMP_Text playerNameText, characterNameText,combatLevelText,healthText, meleeLevelText,rangeLevelText,magicLevelText,meleeAttackText,meleeDefenseText,rangeAttackText,rangeDefenseText,magicAttackText,magicDefenseText,critChanceText,weaponText,shieldText,headText,bodyText,legsText,feetText, goldText;

    [SerializeField]
    private TMP_InputField levelAmountInput, goldAmountInput, healthAmountInput, saveFileNameInput, diceSideAmountInput, diceAmountInput, noteInput;

    [SerializeField]
    private TMP_Dropdown levelTypeDropdown;

    [SerializeField]
    private PlayerCharacter player;

    public GameObject AddItemButtonPrefab { get => addItemButtonPrefab; set => addItemButtonPrefab = value; }
    public GameObject RemoveItemButtonPrefab { get => removeItemButtonPrefab; set => removeItemButtonPrefab = value; }
    public GameObject UseItemButtonPrefab { get => useItemButtonPrefab; set => useItemButtonPrefab = value; }
    public GameObject LoadCharacterButtonPrefab { get => loadCharacterButtonPrefab; set => loadCharacterButtonPrefab = value; }
    public GameObject CharacerItemsContent { get => characterItemsContent; set => characterItemsContent = value; }
    public GameObject LoadCharactersContent { get => loadCharactersContent; set => loadCharactersContent = value; }
    public GameObject CharacterNoteContent { get => characterNoteContent; set => characterNoteContent = value; }
    public GameObject CharacterNotePrefab { get => characterNotePrefab; set => characterNotePrefab = value; }
    public TMP_Text PlayerNameText { get => playerNameText; set => playerNameText = value; }
    public TMP_Text CharacterNameText { get => characterNameText; set => characterNameText = value; }
    public TMP_Text CombatLevelText { get => combatLevelText; set => combatLevelText = value; }
    public TMP_Text HealthText { get => healthText; set => healthText = value; }
    public TMP_Text MeleeLevelText { get => meleeLevelText; set => meleeLevelText = value; }
    public TMP_Text RangeLevelText { get => rangeLevelText; set => rangeLevelText = value; }
    public TMP_Text MagicLevelText { get => magicLevelText; set => magicLevelText = value; }
    public TMP_Text MeleeAttackText { get => meleeAttackText; set => meleeAttackText = value; }
    public TMP_Text MeleeDefenseText { get => meleeDefenseText; set => meleeDefenseText = value; }
    public TMP_Text RangeAttackText { get => rangeAttackText; set => rangeAttackText = value; }
    public TMP_Text RangeDefenseText { get => rangeDefenseText; set => rangeDefenseText = value; }
    public TMP_Text MagicAttackText { get => magicAttackText; set => magicAttackText = value; }
    public TMP_Text MagicDefenseText { get => magicDefenseText; set => magicDefenseText = value; }
    public TMP_Text CritChanceText { get => critChanceText; set => critChanceText = value; }
    public TMP_Text WeaponText { get => weaponText; set => weaponText = value; }
    public TMP_Text ShieldText { get => shieldText; set => shieldText = value; }
    public TMP_Text HeadText { get => headText; set => headText = value; }
    public TMP_Text BodyText { get => bodyText; set => bodyText = value; }
    public TMP_Text LegsText { get => legsText; set => legsText = value; }
    public TMP_Text FeetText { get => feetText; set => feetText = value; }
    public TMP_Text GoldText { get => goldText; set => goldText = value; }
    public TMP_InputField LevelAmountInput { get => levelAmountInput; set => levelAmountInput = value; }
    public TMP_InputField LoseHealthAmountInput { get => healthAmountInput; set => healthAmountInput = value; }
    public TMP_InputField GoldAmountInput { get => goldAmountInput; set => goldAmountInput = value; }
    public TMP_InputField SaveFileNameInput { get => saveFileNameInput; set => saveFileNameInput = value; }
    public TMP_InputField DiceSideAmountInput { get => diceSideAmountInput; set => diceSideAmountInput = value; }
    public TMP_InputField DiceAmountInput { get => diceAmountInput; set => diceAmountInput = value; }
    public TMP_InputField NoteInput { get => noteInput; set => noteInput = value; }
    public TMP_Dropdown LevelTypeDropdown { get => levelTypeDropdown; set => levelTypeDropdown = value; }
    public PlayerCharacter Player { get => player; set => player = value; }

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

    private void Update()
    {
        player = PlayerManager.instance.playerCharacter;
        playerNameText.text = $"Player Name: {player.PlayerName}";
        characterNameText.text = $"Character Name: {player.CharacterName}";
        combatLevelText.text = $"Combat Level: {player.Levels.CombatLevel}";
        meleeLevelText.text = $"Melee Level: {player.Levels.Melee.Level} Exp: {player.Levels.Melee.Exp}/{player.Levels.Melee.ExpTarget}";
        rangeLevelText.text = $"Range Level: {player.Levels.Range.Level} Exp: {player.Levels.Range.Exp}/{player.Levels.Range.ExpTarget}";
        magicLevelText.text = $"Magic Level: {player.Levels.Magic.Level} Exp: {player.Levels.Magic.Exp}/{player.Levels.Magic.ExpTarget}";
        healthText.text = $"Health: {player.Stats.Health.CurrentValue}/{player.Stats.Health.MaxValue}";
        meleeAttackText.text = $"Melee Attack: {player.Stats.MeleeAttack.MaxValue}";
        meleeDefenseText.text = $"Melee Defense: {player.Stats.MeleeDefense.MaxValue}";
        rangeAttackText.text = $"Range Attack: {player.Stats.RangeAttack.MaxValue}";
        rangeDefenseText.text = $"Range Defense: {player.Stats.RangeDefense.MaxValue}";
        magicAttackText.text = $"Magic Attack: {player.Stats.MagicAttack.MaxValue}";
        magicDefenseText.text = $"Magic Defense: {player.Stats.MagicDefense.MaxValue}";
        critChanceText.text = $"Crit Chance: {player.Stats.CritChance.MaxValue}";

        goldText.text = $"Gold: {player.Inventory.Gold}";

        weaponText.text = (player.Equipment[0] != null) ? $"Weapon: {player.Equipment[0].ItemName}" : "Weapon: None";
        shieldText.text = (player.Equipment[1] != null) ? $"Shield: {player.Equipment[1].ItemName}" : "Shield: None";
        headText.text = (player.Equipment[2] != null) ? $"Head: {player.Equipment[2].ItemName}" : "Head: None";
        bodyText.text = (player.Equipment[3] != null) ? $"Body: {player.Equipment[3].ItemName}" : "Body: None";
        legsText.text = (player.Equipment[4] != null) ? $"Legs: {player.Equipment[4].ItemName}" : "Legs: None";
        feetText.text = (player.Equipment[5] != null) ? $"Feet: {player.Equipment[5].ItemName}" : "Feet: None";
    }

    public void ClearItemsContent()
    {
        if (characterItemsContent.transform.childCount <= 0) return;

        for (int i = characterItemsContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(characterItemsContent.transform.GetChild(i).gameObject);
        }
    }

    public void ClearLoadCharacterContent()
    {
        if (loadCharactersContent.transform.childCount <= 0) return;

        for (int i = loadCharactersContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(loadCharactersContent.transform.GetChild(i).gameObject);
        }
    }

    public void ClearCharacterNoteContent()
    {
        if (characterNoteContent.transform.childCount <= 0) return;

        for (int i = characterNoteContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(characterNoteContent.transform.GetChild(i).gameObject);
        }
    }

    public void AddItem()
    {
        ClearItemsContent();
        for (int i = 0; i < ItemDatabase.instance.Items.Count; i++)
        {
            var button = Instantiate(addItemButtonPrefab, characterItemsContent.transform);
            var itemBtnScript = button.GetComponent<CharacterItemButton>();
            if(itemBtnScript != null)
            {
                itemBtnScript.SelectedItemSlot = new InventorySlot { Item = ItemDatabase.instance.Items[i], Amount = 1 };
            }
        }
    }

    public void RemoveItem()
    {
        ClearItemsContent();
        for (int i = 0; i < player.Inventory.Items.Count; i++)
        {
            var button = Instantiate(removeItemButtonPrefab, characterItemsContent.transform);
            var itemBtnScript = button.GetComponent<CharacterItemButton>();
            var itemSlot = player.Inventory.Items[i];
            if (itemBtnScript != null)
            {
                itemBtnScript.SelectedItemSlot = itemSlot;
            }
        }
    }

    public void UseItem()
    {
        ClearItemsContent();
        for (int i = 0; i < player.Inventory.Items.Count; i++)
        {
            var button = Instantiate(useItemButtonPrefab, characterItemsContent.transform);
            var itemBtnScript = button.GetComponent<CharacterItemButton>();
            var itemSlot = player.Inventory.Items[i];
            if (itemBtnScript != null)
            {
                itemBtnScript.SelectedItemSlot = itemSlot;
            }
        }
    }

    public void RemoveAllItems()
    {
        PopupDisplayUI.instance.ShowPopup($"Are you sure you want to remove all items from {player.CharacterName}'s Inventory?", PopupDisplayUI.PopupPosition.Middle,
            () => {
                for (int i = player.Inventory.Items.Count - 1; i >= 0; i--)
                {
                    player.Inventory.Items.RemoveAt(i);
                }
            }, () => { });
    }

    public void SelectEquipment(int slot)
    {
        ClearItemsContent();
        for (int i = 0; i < player.Inventory.Items.Count; i++)
        {
            var equipment = player.Inventory.Items[i].Item as Equipment;
            if(equipment != null)
            {
                if(equipment.Slot == (EquipmentSlot)slot)
                {
                    var button = Instantiate(useItemButtonPrefab, characterItemsContent.transform);
                    var itemBtnScript = button.GetComponent<CharacterItemButton>();
                    var itemSlot = player.Inventory.Items[i];
                    if (itemBtnScript != null)
                    {
                        itemBtnScript.IsSelectEquipment = true;
                        itemBtnScript.SelectedItemSlot = itemSlot;
                    }
                }
            }
        }
    }

    public void GainExp()
    {
        int val = int.Parse(levelAmountInput.text);
        switch (levelTypeDropdown.value)
        {
            //Melee
            case 0:
                player.Levels.Melee.GainExp(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Melee Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Range
            case 1:
                player.Levels.Range.GainExp(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Range Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Magic
            case 2:
                player.Levels.Magic.GainExp(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Magic Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
        }
    }
    
    public void GainGold()
    {
        int val = int.Parse(goldAmountInput.text);
        
        player.Inventory.Gold += val;
        PopupDisplayUI.instance.ShowPopup($"Gained {val} Gold", PopupDisplayUI.PopupPosition.Middle, () => { });
    }
    
    public void SetGold()
    {
        int val = int.Parse(goldAmountInput.text);
        
        player.Inventory.Gold = val;
        PopupDisplayUI.instance.ShowPopup($"Set Gold to {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
    }
    
    public void LoseGold()
    {
        int val = int.Parse(goldAmountInput.text);

        if (player.Inventory.Gold - val > 0)
        {
            player.Inventory.Gold -= val;
            PopupDisplayUI.instance.ShowPopup($"Lose {val} Gold", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        else
        {
            player.Inventory.Gold = 0;
            PopupDisplayUI.instance.ShowPopup($"Lose all Gold", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
    }

    public void LoseExp()
    {
        int val = int.Parse(levelAmountInput.text);
        switch (levelTypeDropdown.value)
        {
            //Melee
            case 0:
                player.Levels.Melee.LoseExp(val);
                PopupDisplayUI.instance.ShowPopup($"Lose Melee Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Range
            case 1:
                player.Levels.Range.LoseExp(val);
                PopupDisplayUI.instance.ShowPopup($"Lose Range Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Magic
            case 2:
                player.Levels.Magic.LoseExp(val);
                PopupDisplayUI.instance.ShowPopup($"Lose Magic Exp by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
        }
    }

    public void GainLevel()
    {
        int val = int.Parse(levelAmountInput.text);
        switch (levelTypeDropdown.value)
        {
            //Melee
            case 0:
                player.Levels.Melee.GainLevel(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Melee Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Range
            case 1:
                player.Levels.Range.GainLevel(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Range Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Magic
            case 2:
                player.Levels.Magic.GainLevel(val);
                PopupDisplayUI.instance.ShowPopup($"Gain Magic Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
        }
    }

    public void LoseLevel()
    {
        int val = int.Parse(levelAmountInput.text);
        switch (levelTypeDropdown.value)
        {
            //Melee
            case 0:
                player.Levels.Melee.LoseLevel(val);
                PopupDisplayUI.instance.ShowPopup($"Lose Melee Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Range
            case 1:
                player.Levels.Range.LoseLevel(val);
                PopupDisplayUI.instance.ShowPopup($"Lose Range Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
            //Magic
            case 2:
                player.Levels.Magic.LoseLevel (val);
                PopupDisplayUI.instance.ShowPopup($"Lose Magic Level by {val}", PopupDisplayUI.PopupPosition.Middle, () => { });
                break;
        }
    }

    public void LoseHealth()
    {
        var val = int.Parse(healthAmountInput.text);
        player.Stats.Health.CurrentValue -= val;
        if (player.Stats.Health.CurrentValue <= 0)
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Loses {val} Health and is now dead", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        else
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Loses {val} Health", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
    }

    public void GainHealth()
    {
        var val = int.Parse(healthAmountInput.text);
        player.Stats.Health.CurrentValue += val;
        if(player.Stats.Health.CurrentValue >= player.Stats.Health.MaxValue)
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Gains {val} Health and is now at full health", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        else
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Gains {val} Health", PopupDisplayUI.PopupPosition.Middle, () => { });

        }
    }
    public void SetHealth()
    {
        var val = int.Parse(healthAmountInput.text);
        player.Stats.Health.CurrentValue = val;
        if (player.Stats.Health.CurrentValue >= player.Stats.Health.MaxValue)
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Set Health to {val} and is now at full health", PopupDisplayUI.PopupPosition.Middle, () => { });
        }
        else
        {
            PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Set Health to {val}", PopupDisplayUI.PopupPosition.Middle, () => { });

        }

    }

    public void RollAttack()
    {
        int calc = 0;
        int critRoll = Random.Range(1, 101);
        if (player.Equipment[0] != null)
        {
            switch (player.Equipment[0].Type)
            {
                case EquipmentType.Melee:
                    calc = Random.Range(0, player.Stats.MeleeAttack.CurrentValue + 1) + Random.Range(0, player.Levels.Melee.Level);
                    break;
                case EquipmentType.Range:
                    calc = Random.Range(0, player.Stats.RangeAttack.CurrentValue + 1) + Random.Range(0, player.Levels.Range.Level);
                    break;
                case EquipmentType.Magic:
                    calc = Random.Range(0, player.Stats.MagicAttack.CurrentValue + 1) + Random.Range(0, player.Levels.Magic.Level);
                    break;
            }

            if(calc > 0)
            {
                if (critRoll <= player.Stats.CritChance.CurrentValue)
                {
                    calc *= 2;
                    PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} {player.Equipment[0].Type} damage critical hit! before subtracting enemy defense", PopupDisplayUI.PopupPosition.Middle, () => { });
                }
                else
                {
                    PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} {player.Equipment[0].Type} damage before subtracting enemy defense", PopupDisplayUI.PopupPosition.Middle, () => { });
                }
            }
            else
            {
                PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} {player.Equipment[0].Type} damage misses!", PopupDisplayUI.PopupPosition.Middle, () => { });
            }
        }
        else
        {

            calc = Random.Range(0, player.Stats.MeleeAttack.CurrentValue + 1) + Random.Range(0, player.Levels.Melee.Level);
            if (calc > 0)
            {
                if (critRoll <= player.Stats.CritChance.CurrentValue)
                {
                    calc *= 2;
                    PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} Melee damage critical hit! before subtracting enemy defense", PopupDisplayUI.PopupPosition.Middle, () => { });
                }
                else
                {
                    PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} Melee damage before subtracting enemy defense", PopupDisplayUI.PopupPosition.Middle, () => { });
                }
            }
            else
            {
                PopupDisplayUI.instance.ShowPopup($"{player.CharacterName} Rolls {calc} Melee damage misses!", PopupDisplayUI.PopupPosition.Middle, () => { });
            }

        }
    }

    public void RollDice()
    {
        int.TryParse(diceSideAmountInput.text, out int sides);
        int.TryParse(diceAmountInput.text, out int amount) ;

        if (sides <= 0)
        {
            PopupDisplayUI.instance.ShowPopup($"Please enter a valid sides number.", PopupDisplayUI.PopupPosition.Middle, (
                () =>
                {
                
                }));
        }
        else if (amount <= 0)
        {
            PopupDisplayUI.instance.ShowPopup($"Please enter a valid amount number.", PopupDisplayUI.PopupPosition.Middle, (
                () =>
                {
                
                }));
        }
        else
        {
            var total = 0;
            for (int i = 0; i < amount; i++)
            {
                total += Random.Range(1, sides + 1);
            }
        
            PopupDisplayUI.instance.ShowPopup($"Rolled {amount} X {sides} sided dice with a total of {total}", PopupDisplayUI.PopupPosition.Middle, (
                () =>
                {
                
                }));
        }
        


    }

    public void SaveCharacter()
    {
        PopupDisplayUI.instance.ShowPopup($"Saved Character {player.CharacterName}", PopupDisplayUI.PopupPosition.Middle, 
            () => {
                if (!string.IsNullOrWhiteSpace(saveFileNameInput.text))
                {
                    var json = JsonConvert.SerializeObject(player, Formatting.Indented);
                    var fullPath = Path.Combine(Application.persistentDataPath, saveFileNameInput.text + ".save");
                    File.WriteAllText(fullPath, json);
                }
            });

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetSaveInputTextToCharacterName()
    {
        if (string.IsNullOrWhiteSpace(saveFileNameInput.text))
        {
            saveFileNameInput.text = player.CharacterName;
        }
    }

    public void LoadNotes()
    {
        ClearCharacterNoteContent();

        for (int i = 0; i < PlayerManager.instance.playerCharacter.Notes.Count; i++)
        {
            var noteBtn = Instantiate(characterNotePrefab, characterNoteContent.transform);
            var uiBtn = noteBtn.GetComponent<NoteUIButton>();
            uiBtn.Note = PlayerManager.instance.playerCharacter.Notes[i];
            uiBtn.NoteText.text = PlayerManager.instance.playerCharacter.Notes[i].NoteText;
        }
    }

    public void AddNote()
    {
        var note = new Note(noteInput.text);
        PlayerManager.instance.playerCharacter.Notes.Add(note);
        var noteBtn = Instantiate(characterNotePrefab, characterNoteContent.transform);
        var uiBtn = noteBtn.GetComponent<NoteUIButton>();
        uiBtn.Note = note;
        uiBtn.NoteText.text = note.NoteText;
    }

    public void RemoveAllNotes()
    {
        PopupDisplayUI.instance.ShowPopup("Are you sure you want to remove all Notes?", PopupDisplayUI.PopupPosition.Middle,
            () =>
            {
                PlayerManager.instance.playerCharacter.Notes.Clear();
                ClearCharacterNoteContent();
            }, () =>
            {
                
            });
    }
}