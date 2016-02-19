﻿using UnityEngine;
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
    public Image RuneSelectedImage;

    public static Dictionary<string, Toggle> runeToggleGroup = new Dictionary<string, Toggle>();

    void Awake()
    {
        RuneWindow.selectedRunes.Add(this);
    }

    void Start()
    {
    }

    public void SortRunes()
    {
        foreach (var rune in PlayFabDataStore.playerAllRunes)
        {
            if (runeId == rune.Value.itemId && rune.Value.active == "1" && rune.Value.itemClass == "Skill")
            {
                if(!PlayFabDataStore.playerActiveSkillRunes.ContainsKey(skillSlot))
                {
                    PlayFabDataStore.playerActiveSkillRunes.Add(skillSlot, rune.Value.itemId);
                    PlayFabDataStore.playerActiveRuneImages.Add(skillSlot, runeImage.sprite);
                }
                else
                {
                    PlayFabDataStore.playerActiveSkillRunes[skillSlot] = rune.Value.itemId;
                    PlayFabDataStore.playerActiveRuneImages[skillSlot] = runeImage.sprite;
                }
                runeToggle.interactable = true;
                runeDisabledImage.enabled = false;
                //RuneSelectedImage.gameObject.SetActive(true);
                runeToggle.isOn = true;
            }
            if (runeId == rune.Value.itemId && rune.Value.active == "1" && rune.Value.itemClass == "Modifier")
            {
                if(!PlayFabDataStore.playerActiveModifierRunes.ContainsKey(runeId))
                {
                    PlayFabDataStore.playerActiveModifierRunes.Add(runeId, skillSlot);
                    runeToggle.interactable = true;
                    runeDisabledImage.enabled = false;
                    //RuneSelectedImage.gameObject.SetActive(true);
                    runeToggle.isOn = true;
                }
                
            }
            if (runeId == rune.Value.itemId && rune.Value.active == "0")
            {
                runeToggle.interactable = true;
                runeDisabledImage.enabled = false;
            }
        }
    }

    public void SelectRune()
    {
        Debug.Log(gameObject + " " + runeToggle.isOn);
        if (!runeToggle.isOn)
        {
            //runeToggle.isOn = false;
            PlayFabApiCalls.SetCustomDataOnItem("Active", "0", PlayFabDataStore.playerAllRunes[runeId].instanceId);  
        }

        //Do these for selected rune. It modifies or adds the rune to active dictionary
        if (runeClass == "Skill" && runeToggle.isOn )
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
            runeToggle.isOn = true;
            PlayFabApiCalls.SetCustomDataOnItem("Active", "1", PlayFabDataStore.playerAllRunes[runeId].instanceId);
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
            runeToggle.isOn = true;
        }
        if (runeClass == "Modifier" && !runeToggle.isOn)
        {
            if (PlayFabDataStore.playerActiveModifierRunes.ContainsKey(runeId))
            {
                PlayFabDataStore.playerActiveModifierRunes.Remove(runeId);
            }
        }
        

        //Debug.Log("RuneSelect");
        ActionBar.RefreshActionBar();
    }

    public void ShowTooltip()
    {
        Invoke("SetTooltipData", 0.25f);
    }

    public void HideTooltip()
    {
        CancelInvoke("SetTooltipData");
        UITooltip.Hide();
    }

    void SetTooltipData()
    {  
        UITooltip.AddTitle(PlayFabDataStore.playerAllRunes[runeId].displayName);
        UITooltip.AnchorToRect(this.transform as RectTransform);
        UITooltip.Show();
    }

}
