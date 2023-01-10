using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterEquipmentPanel : MonoBehaviour
{
    [SerializeField]
    private EquipmentSlot slot;

    [SerializeField]
    private TMP_Text equipmentNameText;

    private PlayerCharacter player;

    public EquipmentSlot Slot { get => slot; set => slot = value; }

    private void Start()
    {
        player = PlayerManager.instance.playerCharacter;
    }

    private void Update()
    {
        if(player != null)
        {
            equipmentNameText.text = (player.Equipment[(int)slot] != null) ? $"Equipped: {player.Equipment[(int)slot].ItemName}" : "Equipped: None";
        }
    }

    public void RemoveEquipment()
    {
        if(player != null)
        {
            if(player.Equipment[(int)slot] != null)
            {
                player.Equipment[(int)slot].Use(player);
            }
        }
    }

}
