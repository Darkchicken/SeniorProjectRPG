using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class CheatPanel : MonoBehaviour
{
    public InputField grantItemText;
    private string itemInstanceId;

    public void GrantItem()
    {
        string[] items = { grantItemText.text };
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

    public void GetCharacterRunes()
    {
        PlayFabApiCalls.GetAllCharacterRunes();
    }

}
