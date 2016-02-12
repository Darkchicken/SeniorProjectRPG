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
        string[] items = { itemID };
        PlayFabApiCalls.GrantItemsToCharacter(items);
    }

    public void RevokeItem()
    {
        PlayFabApiCalls.RevokeInventoryItem(itemInstanceId);
    }

    public void UpdateCharacterData()
    {
        PlayFabApiCalls.UpdateCharacterData();
    }

    public void ListCharacterInventory()
    {
        
    }
}
