using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class CharacterLoadButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fileNameText, playerNameText, characterNameText, combatLevelText, meleeLevelText, rangeLevelText, magicLevelText;
    [SerializeField]
    private PlayerCharacter character;
    [SerializeField]
    private string fileName;

    public TMP_Text FileNameText { get => fileNameText; set => fileNameText = value; }
    public TMP_Text PlayerNameText { get => playerNameText; set => playerNameText = value; }
    public TMP_Text CharacterNameText { get => characterNameText; set => characterNameText = value; }
    public TMP_Text CombatLevelText { get => combatLevelText; set => combatLevelText = value; }
    public TMP_Text MeleeLevelText { get => meleeLevelText; set => meleeLevelText = value; }
    public TMP_Text RangeLevelText { get => rangeLevelText; set => rangeLevelText = value; }
    public TMP_Text MagicLevelText { get => magicLevelText; set => magicLevelText = value; }
    public PlayerCharacter Character { get => character; set => character = value; }
    public string FileName { get => fileName; set => fileName = value; }

    // Update is called once per frame
    public void Update()
    {
        if (character != null && !string.IsNullOrWhiteSpace(fileName))
        {
            fileNameText.text = $"Filename: {fileName}";
            playerNameText.text = $"Player Name: {character.PlayerName}";
            characterNameText.text = $"Character Name: {character.CharacterName}";
            combatLevelText.text = $"Combat Level: {character.Levels.CombatLevel}";
            meleeLevelText.text = $"Melee Level: {character.Levels.Melee.Level}";
            rangeLevelText.text = $"Range Level: {character.Levels.Range.Level}";
            magicLevelText.text = $"Magic Level: {character.Levels.Magic.Level}";

        }
    }

    public void DeleteSave()
    {
        if (character != null)
        {
            try
            {
                PopupDisplayUI.instance.ShowPopup($"Confirm Delete Character: {character.CharacterName}?", PopupDisplayUI.PopupPosition.Middle,
                    () => {
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
                            File.Delete(fullPath);
                            if(PlayerCharacterMenuController.instance != null)
                            {
                                PlayerCharacterMenuController.instance.LoadCharacters();
                            }
                            if (MainMenuUIController.instance != null)
                            {
                                MainMenuUIController.instance.LoadCharacters();
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

    public void LoadCharacter(string sceneName = "")
    {
        if(character != null)
        {
            PopupDisplayUI.instance.ShowPopup($"Loaded Character: {character.CharacterName}", PopupDisplayUI.PopupPosition.Middle, 
                ()=> {
                    PlayerManager.instance.playerCharacter = character;

                    if (!string.IsNullOrWhiteSpace(sceneName) && SceneManager.GetActiveScene().name != sceneName)
                    {
                        SceneManager.LoadScene(sceneName);
                    }
                });

        }
    }
}
