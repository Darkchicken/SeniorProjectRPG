﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabApiCalls : MonoBehaviour
{

    //Login to playfab/game
    public static void PlayFabLogin(string username, string password)
    {
        var loginRequest = new LoginWithPlayFabRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, (result) =>
        {
            PlayFabDataStore.playFabId = result.PlayFabId;
            PlayFabDataStore.sessionTicket = result.SessionTicket;
            //PlayFabUserLogin.playfabUserLogin.Authentication("AUTHENTICATING...", 1);
            GetPhotonToken();
        }, (error) =>
        {
            PlayFabUserLogin.playfabUserLogin.Authentication(error.ErrorMessage.ToString().ToUpper(), 3);
        });
    }
    
    //Access the newest version of cloud script
    public static void PlayFabInitialize()
    {
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
    }

    //Get Photon Token from playfab
    public static void GetPhotonToken()
    {
        var request = new GetPhotonAuthenticationTokenRequest();
        {
            request.PhotonApplicationId = "67a8e458-b05b-463b-9abe-ce766a75b832".Trim();
        }

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, (result) =>
        {
            string photonToken = result.PhotonCustomAuthenticationToken;
            Debug.Log(string.Format("Yay, logged in in session token: {0}", photonToken));
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
            PhotonNetwork.AuthValues.AddAuthParameter("username", PlayFabDataStore.playFabId);
            PhotonNetwork.AuthValues.AddAuthParameter("Token", result.PhotonCustomAuthenticationToken);
            PhotonNetwork.AuthValues.UserId = PlayFabDataStore.playFabId;
            PhotonNetwork.ConnectUsingSettings("1.0");
            PlayFabUserLogin.playfabUserLogin.Authentication("AUTHENTICATING...", 1);
            //PlayFabUserLogin.playfabUserLogin.Authentication("SUCCESS!", 2); //change the text of authentication text
        }, (error) =>
        {
            PlayFabUserLogin.playfabUserLogin.Authentication(error.ErrorMessage.ToString().ToUpper(), 3);
            //PlayFabErrorHandler.HandlePlayFabError(error);
        });
    }

    //Receives all characters belong to the user
    public static void GetAllUsersCharacters()
    {
        var request = new ListUsersCharactersRequest()
        {
            PlayFabId = PlayFabDataStore.playFabId
        };

        PlayFabClientAPI.GetAllUsersCharacters(request, (result) =>
        {
            foreach (var character in result.Characters)
            {
                PlayFabDataStore.characters.Add(character.CharacterName, character.CharacterId);
            }
        }, (error) =>
        {
            Debug.Log("Can't retrieve character!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Create new character
    public static void CreateNewCharacter(string name)
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "newCharacter",
            Params = new { characterName = name, characterType = "Player" }//set to whatever default class is
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
       
        }, (error) =>
        {
            Debug.Log("Character not created!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Get custom data of the character and set them to their locals
    public static void GetCharacterData()
    {
        var request = new GetCharacterDataRequest()
        {
            CharacterId = PlayFabDataStore.characterId
        };
        PlayFabClientAPI.GetCharacterData(request, (result) =>
        {
            Debug.Log("Data successfully retrieved!");
            PlayFabDataStore.playerLevel = int.Parse(result.Data["Level"].Value);
            PlayFabDataStore.playerExperience = int.Parse(result.Data["Experience"].Value);
            PlayFabDataStore.playerHealth = int.Parse(result.Data["Health"].Value);
            PlayFabDataStore.playerResource = int.Parse(result.Data["Resource"].Value);
            PlayFabDataStore.playerStrength = int.Parse(result.Data["Strength"].Value);
            PlayFabDataStore.playerIntellect = int.Parse(result.Data["Intellect"].Value);
            PlayFabDataStore.playerDexterity = int.Parse(result.Data["Dexterity"].Value);
            PlayFabDataStore.playerVitality = int.Parse(result.Data["Vitality"].Value);
            PlayFabDataStore.playerCriticalChance = int.Parse(result.Data["Critical Chance"].Value);
            PlayFabDataStore.playerWeaponDamage = int.Parse(result.Data["Weapon Damage"].Value);

        }, (error) =>
        {
            Debug.Log("Character data request failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Updates character's custom data to playfab
    public static void UpdateCharacterData()
    {
        var request = new UpdateCharacterDataRequest()
        {
            CharacterId = PlayFabDataStore.characterId,
            Data = PlayFabDataStore.playerData
        };
        PlayFabClientAPI.UpdateCharacterData(request, (result) =>
        {
            Debug.Log("Stats Updated!");
        }, (error) =>
        {
            Debug.Log("Stats Failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Grant character the items in the array
    public static void GrantRunesToCharacter(string[] items)
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "grantItemsToCharacter",
            Params = new { playFabId = PlayFabDataStore.playFabId, characterId = PlayFabDataStore.characterId, items = items }
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            string[] splitResult = result.ResultsEncoded.Split('"'); //19th element is the itemInstanceId
            SetCustomDataOnItem("Active", "0", splitResult[19]);    
        },
        (error) =>
        {
            Debug.Log("Item not Granted!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Removes the specific item from the users inventory
    public static void RevokeInventoryItem(string itemInstanceId)
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "revokeInventoryItem",
            Params = new { characterId = PlayFabDataStore.characterId, itemId = itemInstanceId }
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            Debug.Log(result.Results);
        },
        (error) =>
        {
            Debug.Log("Item not Revoked!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    //Receives all items in characters inventory
    public static void GetCharacterInventory()
    {
        var request = new GetCharacterInventoryRequest()
        {
            CharacterId = PlayFabDataStore.characterId
        };

        PlayFabClientAPI.GetCharacterInventory(request, (result) =>
        {
            Debug.Log("Inventory Count: " + result.Inventory.Count);
            foreach (var item in result.Inventory)
            {
                Debug.Log(item.DisplayName);
                Debug.Log(item.ItemInstanceId);
            }
        }, (error) =>
        {
            Debug.Log("Listing Inventory Failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    public static void GetAllCharacterRunes()
    {
        var request = new GetCharacterInventoryRequest()
        {
            CharacterId = PlayFabDataStore.characterId
        };
        PlayFabClientAPI.GetCharacterInventory(request, (result) =>
        {
            Debug.Log(result.Inventory.Count);
            
            foreach (var item in result.Inventory)
            {
                if(!PlayFabDataStore.playerAllRunes.ContainsKey(item.ItemId))
                {
                    PlayFabDataStore.playerAllRunes.Add(item.ItemId, new Rune(item.ItemId, item.ItemInstanceId, item.ItemClass, item.DisplayName, item.CustomData["Active"]));
                }
                
            }
            Debug.Log("Runes are retrieved");
            RuneWindow.SortAllRunes();
        },
        (error) =>
        {
            Debug.Log("Catalog can't retrieved!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

    }

    public static void SetCustomDataOnItem(string key, string value, string itemInstanceId)
    {
        Dictionary<string, string> customData = new Dictionary<string, string>();
        customData.Add(key, value);
        var request = new RunCloudScriptRequest()
        {
            ActionId = "setCustomDataToGrantedItem",
            Params = new { characterId = PlayFabDataStore.characterId, itemInstanceId = itemInstanceId, data = customData }
        };

        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            Debug.Log("Custom data set!");
            //GetAllCharacterRunes();
        },
        (error) =>
        {
            Debug.Log("Cant set custom data");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

}
