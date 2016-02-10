﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabMainMenu : MonoBehaviour {

    public UICharacterSelect_List listComponent;
    //public Text characterInfo;
    public Button playButton;
    public Button createButton;
    //public Button updateButton;
    //public Text playerButtonText;
    //public InputField characterNameText;
    //public Canvas cheatPanel;
    

    //private bool isCharacterExist;


    void Update()
    {
        /*if(Input.GetKeyDown("1"))
        {
            cheatPanel.gameObject.SetActive(!cheatPanel.gameObject.activeInHierarchy);
        }*/
    }
    void OnEnable()
    {
        //////access newest version of cloud script
        var cloudRequest = new GetCloudScriptUrlRequest()
        {
            Testing = false
        };

        PlayFabClientAPI.GetCloudScriptUrl(cloudRequest, (result) =>
        {
            Debug.Log("URL is set");
        },
        (error) =>
        {
            Debug.Log("Failed to retrieve Cloud Script URL");
        });
        //////////////////////////////////////////

        playButton.onClick.AddListener(Play);
        createButton.onClick.AddListener(CreateNewCharacter);

        var request = new ListUsersCharactersRequest()
        {
            PlayFabId = PlayFabDataStore.playFabId
        };

        PlayFabClientAPI.GetAllUsersCharacters(request, (result) =>
        {

            if (this.listComponent != null)
            {
                // Empty out the list
                if (true)
                {
                    foreach (Transform trans in listComponent.transform)
                    {
                        Destroy(trans.gameObject);
                    }
                }
                foreach (var character in result.Characters)
                {
                    listComponent.AddCharacter(character.CharacterName, "", character.CharacterType, 1);
                    PlayFabDataStore.characterId = character.CharacterId;
                    PlayFabDataStore.characterName = character.CharacterName;
                }

            }

            

        }, (error) =>
        {
            Debug.Log("Can't retrieve character!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

    }

    void Play()
    {
        SceneManager.LoadScene("TestMovement");
        //PhotonNetwork.LoadLevel("TestMovement");
    }

    void CreateNewCharacter()
    {

    }

   /* void CreateNewCharacter()
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "newCharacter",
            Params = new { characterName = characterNameText.text, characterType = "Player", playerData = PlayFabDataStore.playerData }//set to whatever default class is
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
             characterInfo.text = characterNameText.text;
             characterNameText.gameObject.SetActive(false);
             characterInfo.gameObject.SetActive(true);
             PlayFabDataStore.characterId = result.Results.;
             Debug.Log("Character ID result" + result.ResultsEncoded);
             Debug.Log("Character ID" + PlayFabDataStore.characterId);
             playerButtonText.text = "Enter World";
            OnEnable();
            isCharacterExist = true;
            
        }, (error) =>
        {
            Debug.Log("Character not created!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

    }
    */
  
    
    
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
