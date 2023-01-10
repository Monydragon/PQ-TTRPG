using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerCharacter playerCharacter;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        LevelHandler.onLevelChanged += CharacterLevelChanged;
    }

    private void OnDisable()
    {
        LevelHandler.onLevelChanged -= CharacterLevelChanged;
    }

    public void CharacterLevelChanged(LevelHandler handler, int level)
    {
        if(playerCharacter != null)
        {
            if(playerCharacter.Levels.Melee == handler)
            {
                playerCharacter.Stats.MeleeAttack.MaxValue = level;
                playerCharacter.Stats.MeleeDefense.MaxValue = level;
            }
            if(playerCharacter.Levels.Range == handler)
            {
                playerCharacter.Stats.RangeAttack.MaxValue = level;
                playerCharacter.Stats.RangeDefense.MaxValue = level;

            }
            if (playerCharacter.Levels.Magic == handler)
            {
                playerCharacter.Stats.MagicAttack.MaxValue = level;
                playerCharacter.Stats.MagicDefense.MaxValue = level;

            }
            if (playerCharacter.Levels.CombatLevel > 1)
            {
                playerCharacter.Stats.Health.MaxValue = (100 + (5 * playerCharacter.Levels.CombatLevel));
            }
            else
            {
                playerCharacter.Stats.Health.MaxValue = 100;
            }

            for (int i = 0; i < playerCharacter.Equipment.Length; i++)
            {
                var equip = playerCharacter.Equipment[i];
                if(equip != null)
                {
                    playerCharacter.Stats.Health.MaxValue += equip.Stats.Health.MaxValue;
                    playerCharacter.Stats.MeleeAttack.MaxValue += equip.Stats.MeleeAttack.MaxValue;
                    playerCharacter.Stats.MeleeDefense.MaxValue += equip.Stats.MeleeDefense.MaxValue;
                    playerCharacter.Stats.RangeAttack.MaxValue += equip.Stats.RangeAttack.MaxValue;
                    playerCharacter.Stats.RangeDefense.MaxValue += equip.Stats.RangeDefense.MaxValue;
                    playerCharacter.Stats.MagicAttack.MaxValue += equip.Stats.MagicAttack.MaxValue;
                    playerCharacter.Stats.MagicDefense.MaxValue += equip.Stats.MagicDefense.MaxValue;
                }
            }
        }
    }
}
