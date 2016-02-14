using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RuneSelect : MonoBehaviour
{
    public string runeId;
    public string runeClass;
    public int skillSlot;
    public Toggle runeToggle;
    public Image runeImage;
    public Image runeDisabledImage;

    public static Dictionary<string, Toggle> runeToggleGroup = new Dictionary<string, Toggle>();

    void OnEnable()
    {
        foreach(var rune in PlayFabDataStore.playerAllRunes)
        {
            if(runeId == rune.itemId && rune.active == "1" && rune.itemClass == runeClass)
            {
                PlayFabDataStore.playerActiveSkillRunes.Add(skillSlot, rune.itemId);
                PlayFabDataStore.playerActiveRuneImages.Add(skillSlot, runeImage.sprite);
                runeToggle.interactable = true;
                runeDisabledImage.enabled = false;
                runeToggle.isOn = true;
            }
            if(runeId == rune.itemId && rune.active == "1" && rune.itemClass == runeClass)
            {
                PlayFabDataStore.playerActiveModifierRunes.Add(runeId, skillSlot);
                runeToggle.interactable = true;
                runeDisabledImage.enabled = false;
                runeToggle.isOn = true;
            }
            if(runeId == rune.itemId && rune.active == "0")
            {
                runeToggle.interactable = true;
                runeDisabledImage.enabled = false;
            }
        }
    }

    public void SelectRune()
    {
        //Do these for selected rune. It modifies or adds the rune to active dictionary
        if(runeClass == "Skill" && runeToggle.isOn )
        {
            if(PlayFabDataStore.playerActiveSkillRunes.ContainsKey(skillSlot))
            {
                PlayFabDataStore.playerActiveSkillRunes[skillSlot] = runeId;
                PlayFabDataStore.playerActiveRuneImages[skillSlot] = runeImage.sprite;
            }
            else
            {
                PlayFabDataStore.playerActiveSkillRunes.Add(skillSlot, runeId);
                PlayFabDataStore.playerActiveRuneImages.Add(skillSlot, runeImage.sprite);
            }  
        }
        if (runeClass == "Modifier" && runeToggle.isOn)
        {
            if(PlayFabDataStore.playerActiveModifierRunes.ContainsKey(runeId))
            {
                PlayFabDataStore.playerActiveModifierRunes[runeId] = skillSlot;
            }
            else
            {
                PlayFabDataStore.playerActiveModifierRunes.Add(runeId, skillSlot);
            }  
        }
        //Do these for unselected rune. Remove it from the active dictionary for only Modifiers
        if (runeClass == "Modifier" && !runeToggle.isOn)
        {
            if (PlayFabDataStore.playerActiveModifierRunes.ContainsKey(runeId))
            {
                PlayFabDataStore.playerActiveModifierRunes.Remove(runeId);
            }
        }
        Debug.Log("RuneSelect");
        ActionBar.RefreshActionBar();

    }

}
