using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabMainMenu : MonoBehaviour {

    public Text characterInfo;
    public Button playerButton;
    public Button updateButton;
    public Text playerButtonText;
    public InputField characterNameText;
    public Canvas cheatPanel;
    

    private bool isCharacterExist;


    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            cheatPanel.gameObject.SetActive(!cheatPanel.gameObject.activeInHierarchy);
        }
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

        playerButton.onClick.AddListener(CreateNewEnterWorld);

        var request = new ListUsersCharactersRequest()
        {
            PlayFabId = PlayFabDataStore.playFabId
        };

        PlayFabClientAPI.GetAllUsersCharacters(request, (result) =>
        {
            if (result.Characters.Count == 0)
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
        }, (error) =>
        {
            Debug.Log("Can't retrieve character!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

    }

    void CreateNewEnterWorld()
    {
        if(isCharacterExist)
        {
            SceneManager.LoadScene("TestMovement");
        }
        else
        {
            CreateNewCharacter();
        }
    }

    void CreateNewCharacter()
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "newCharacter",
            Params = new { characterName = characterNameText.text, characterType = "Player" }//set to whatever default class is
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            characterInfo.text = characterNameText.text;
            characterNameText.gameObject.SetActive(false);
            characterInfo.gameObject.SetActive(true);
            PlayFabDataStore.characterId = result.Results.ToString();
            Debug.Log("Character ID" + PlayFabDataStore.characterId);
            playerButtonText.text = "Enter World";
            isCharacterExist = true;
            
        }, (error) =>
        {
            Debug.Log("Character not created!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

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
