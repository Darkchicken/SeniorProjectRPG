using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabMainMenu : MonoBehaviour {

    public Text characterInfo;
    public Button playerButton;
    public Text playerButtonText;
    public InputField characterNameText;

    private bool isCharacterExist;

    void OnEnable()
    {
        playerButton.onClick.AddListener(LoginCreateNew);
        
        var request = new ListUsersCharactersRequest()
        {
            PlayFabId = PlayFabDataStore.playFabId
        };

        PlayFabClientAPI.GetAllUsersCharacters(request, CharacterDataResult, Error);

    }

    void CharacterDataResult(ListUsersCharactersResult result)
    {
        //Debug.Log(result.Characters.Count);
        if(result.Characters.Count == 0)
        {
            characterInfo.text = "Player not found!";
            playerButtonText.text = "Create New";
            characterNameText.gameObject.SetActive(true);
            isCharacterExist = false;
            
        }
        else
        {
            characterInfo.text = "" + result.Characters[0].CharacterName;
            characterInfo.gameObject.SetActive(true);
            characterNameText.gameObject.SetActive(false);
            playerButtonText.text = "Enter World";
            isCharacterExist = true;
            PlayFabDataStore.characterId = result.Characters[0].CharacterId;
        }

    }

    void Error(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.ErrorDetails);
    }

    void LoginCreateNew()
    {
        if(isCharacterExist)
        {
            SetCharacterData();
        }
        else
        {
            CreateNewCharacter();
        }
    }

    void CreateNewCharacter()
    {
        var request = new GrantCharacterToUserRequest()
        {
            CatalogVersion = "Character",
            ItemId = "rpg_character",
            CharacterName = characterNameText.text
        };

        PlayFabClientAPI.GrantCharacterToUser(request, GrantCharacterToUser, Error);
    }

    void GrantCharacterToUser(GrantCharacterToUserResult result)
    {
        if(result.Result)
        {
            characterInfo.text = characterNameText.text;
            characterNameText.gameObject.SetActive(false);
            characterInfo.gameObject.SetActive(true);
            playerButtonText.text = "Enter World";
            isCharacterExist = true;
            
        }
        else
        {
            Debug.Log("Character not created!");
        }
    }

    void SetCharacterData()
    {
        Dictionary<string, string> characterData = new Dictionary<string, string>();
        characterData.Add("Level", "2");
        var request = new UpdateCharacterDataRequest()
        { 
            CharacterId = PlayFabDataStore.characterId,
            Data = characterData
        };

        PlayFabClientAPI.UpdateCharacterData(request, UpdateCharacterData, Error);
    }

    void UpdateCharacterData(UpdateCharacterDataResult result)
    {
        Debug.Log("Level Set!");
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
