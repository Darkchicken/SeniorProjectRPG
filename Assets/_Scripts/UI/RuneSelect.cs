using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RuneSelect : MonoBehaviour
{
    public string runeId;
    public int skillSlot;
    public Toggle runeToggle;
    public Image runeDisabledImage;

    public static Dictionary<string, Toggle> runeToggleGroup = new Dictionary<string, Toggle>();

    void OnEnable()
    {
        foreach(var rune in PlayFabDataStore.playerAllRunes)
        {
            if(runeId == rune.itemId && rune.active == "1" && rune.itemClass == "Skill")
            {
                PlayFabDataStore.playerActiveSkillRunes.Add(skillSlot, rune.itemId);
                runeToggle.interactable = true;
                //runeDisabledImage.gameObject.SetActive(true);
                runeDisabledImage.enabled = false;
                runeToggle.isOn = true;
            }
            if(runeId == rune.itemId && rune.active == "1" && rune.itemClass == "Modifier")
            {
                PlayFabDataStore.playerActiveModifierRunes.Add(runeId, skillSlot);
                runeToggle.interactable = true;
                //runeDisabledImage.gameObject.SetActive(true);
                runeDisabledImage.enabled = false;
                runeToggle.isOn = true;
            }
            if(runeId == rune.itemId && rune.active == "0")
            {
                runeToggle.interactable = true;
                //runeDisabledImage.gameObject.SetActive(true);
                runeDisabledImage.enabled = false;
            }
        }
    }

}
