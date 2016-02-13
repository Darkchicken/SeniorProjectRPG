using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RuneWindow : MonoBehaviour
{
    public string runeId;
    public Toggle runeToggle;
    public Image runeDisabledImage;

    


    void Start()
    {
        RuneSelect.runeToggleGroup.Add(runeId, runeToggle);

        //Invoke("PlaceRunes", 1);
    }

    public void LoadRunes()
    {
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /*foreach (var rune in PlayFabDataStore.playerSkillRunes)
        {
            if(runeToggleGroup.ContainsKey(rune.Key))
            {
                Debug.Log(rune.Key);
                runeToggleGroup[rune.Key].interactable = true;
                runeDisabledImage.gameObject.SetActive(false);
            }
        }

        foreach (var rune in PlayFabDataStore.playerModifierRunes)
        {
            if (runeToggleGroup.ContainsKey(rune.Key))
            {
                Debug.Log(rune.Key);
                runeToggleGroup[rune.Key].interactable = true;
            }
        }

        foreach (var rune in PlayFabDataStore.playerActiveSkillRunes)
        {
            if (runeToggleGroup.ContainsKey(rune.Value))
            {
                runeToggleGroup[rune.Value].interactable = true;
            }
        }

        foreach (var rune in PlayFabDataStore.playerActiveModifierRunes)
        {
            if (runeToggleGroup.ContainsKey(rune.Key))
            {
                Debug.Log(rune.Key);
                runeToggleGroup[rune.Key].isOn = true;
            }
        }*/


    }



}
