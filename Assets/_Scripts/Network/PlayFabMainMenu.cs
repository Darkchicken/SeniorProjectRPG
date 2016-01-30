using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabMainMenu : MonoBehaviour {

    public Text characterInfo;
    public Button playerButton;
    public Text playerButtonText;

    private bool isCharacterExist;

    void OnEnable()
    {
        playerButton.onClick.AddListener(LoginCreateNew);
        
        var request = new ListUsersCharactersRequest()
        {
            PlayFabId = PlayFabDataStore.PlayFabId
        };

        PlayFabClientAPI.GetAllUsersCharacters(request, CharacterDataResult, CharacterDataError);

    }

    void CharacterDataResult(ListUsersCharactersResult result)
    {
        //Debug.Log(result.Characters.Count);
        if(result.Characters.Count == 0)
        {
            characterInfo.text = "Player not found!";
            playerButtonText.text = "Create New";
            isCharacterExist = false;
            
        }
        else
        {
            characterInfo.text = "" + result.Characters[0].CharacterName;
            playerButtonText.text = "Login";
            isCharacterExist = true;
            PlayFabDataStore.characterID = result.Characters[0].CharacterId;
        }

    }

    void CharacterDataError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    void LoginCreateNew()
    {
        if(isCharacterExist)
        {

        }
        else
        {
            CreateNewCharacter();
        }
    }

    void CreateNewCharacter()
    {

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
