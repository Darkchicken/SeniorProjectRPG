using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabMainMenu : MonoBehaviour
{
    public UICharacterSelect_List characterList;
    public ToggleGroup characterListToggle;
    public Canvas loading;

    public static PlayFabMainMenu playfabMainMenu;
    private bool isCharacterSelected;

    void Awake()
    {
        playfabMainMenu = this;
    }
    void OnEnable()
    {

        //Access the newest version of cloud script
        PlayFabApiCalls.PlayFabInitialize();

        //Receives all characters belong to the user
        PlayFabApiCalls.GetAllUsersCharacters();

        Invoke("ListCharacters", 1);
    }

    public void ListCharacters()
    {
        Debug.Log(PlayFabDataStore.characters.Count);
        //Empty out the character list
        foreach (Transform character in characterList.transform)
        {
            Destroy(character.gameObject);
        }
        Debug.Log("list characters");
        foreach(var character in PlayFabDataStore.characters)
        {
            characterList.AddCharacter(character.Key, "", "", 1);
        }

        Invoke("ToggleFirstCharacter", 0.2f);

    }

    void ToggleFirstCharacter()
    {
        characterListToggle.gameObject.GetComponentInChildren<UICharacterSelect_Unit>().isOn = true;
        PlayFabDataStore.characterName = GetComponentInChildren<CharacterSelect>().characterName.text;
        PlayFabDataStore.characterId = PlayFabDataStore.characters[GetComponentInChildren<CharacterSelect>().characterName.text];
        Debug.Log(PlayFabDataStore.characterId);
    }

    public void Play()
    {
        PhotonNetwork.LoadLevel("TestMovement");
    }

    public void GetAllRunes()
    {
        PlayFabApiCalls.GetAllCharacterRunes();
    }






    
   
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /*void OnEnable()
    {
        //remove any exiting items in the container
        var items = StoreItemContainer.GetComponentsInChildren<Item>();
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }

        //populate the container with new items.
        foreach (var item in PlayFabDataStore.Store)
        {
            //find associated catalog item. 
            var catItem = PlayFabDataStore.Catalog.Find((ci) => { return ci.ItemId == item.ItemId; });
            if (catItem == null) { continue; }

            var storeItem = Instantiate(StoreItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            //If for some reason Instantiate fails, break;
            if (storeItem == null) { continue; }

            var storeItemClass = storeItem.GetComponent<Item>();
            storeItemClass.StoreItem = item;
            storeItemClass.ItemDisplay.text = catItem.DisplayName;
            //storeItemClass.ItemDesc.text = catItem.Description;
            //storeItemClass.BuyButton.GetComponentInChildren<Text>().text = string.Format("Buy for {0} Gold", item.VirtualCurrencyPrices["GO"]);
            //storeItem.transform.SetParent(StoreItemContainer.transform);
            //We do this so that the UI doesn't get a distored scale (kinda a unity bug)
            storeItem.transform.localScale = Vector3.one;
        }

    }*/
}
