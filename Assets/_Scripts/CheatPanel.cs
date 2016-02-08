using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class CheatPanel : MonoBehaviour
{
    private string itemInstanceId;

    public void GrantItem(string itemID)
    {
        string[] item = { itemID };
        var request = new RunCloudScriptRequest()
        {
            ActionId = "grantItemsToCharacter",
            Params = new {catalogVersion = "Runes", playFabId = PlayFabDataStore.playFabId, characterId = PlayFabDataStore.characterId, items = item}
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            Debug.Log(itemID + " Granted!");
            Debug.Log(result.Results);
            
        },
        (error) =>
        {
            Debug.Log("Item not Granted!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    public void RevokeItem()
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

    public void UpdateCharacterData()
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

    public void ListCharacterInventory()
    {
        var request = new GetCharacterInventoryRequest()
        {
            CharacterId = PlayFabDataStore.characterId
        };
        
        PlayFabClientAPI.GetCharacterInventory(request, (result) =>
        {
            itemInstanceId = result.Inventory[0].ItemInstanceId; // just to see if revoke item working
            Debug.Log("Inventory Count: " + result.Inventory.Count);
            foreach (var item in result.Inventory)
            {
                Debug.Log(item.DisplayName);
                Debug.Log(item.ItemInstanceId);
                //Debug.Log(item.CustomData["Active"]);
            }
        }, (error) =>
        {
            Debug.Log("Listing Inventory Failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }

    public void SetCustomDataOnItem()
    {
        var request = new RunCloudScriptRequest()
        {
            ActionId = "setCustomDataToGrantedItem",
            Params = new { characterId = PlayFabDataStore.characterId }
        };
        PlayFabClientAPI.RunCloudScript(request, (result) =>
        {
            Debug.Log("Custom Data Set!");
        },
        (error) =>
        {
            Debug.Log("Item not Revoked!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }
}
