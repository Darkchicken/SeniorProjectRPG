using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabDataStore : MonoBehaviour
{
    public string titleId;
    public static string sessionTicket;
    public static string playFabId;
    public static string characterId;
    public static List<CatalogItem> Catalog;
    public static List<StoreItem> Store;
    public static Queue<StartPurchaseResult> Orders = new Queue<StartPurchaseResult>();
    public Text[] playerStats;
    


    void Awake()
    {
        //PlayFabSettings.TitleId = titleId; //hardcoded into the settings.
    }

    public void TogglePlayerStatsPanel()
    {
        var request = new GetCharacterDataRequest()
        {
            CharacterId = characterId
        };
        PlayFabClientAPI.GetCharacterData(request, (result) =>
        {
            Debug.Log("Data successfully retrieved!");

            playerStats[0].text = "Level: " + result.Data["Level"].Value;
            playerStats[1].text = "Strength: " + result.Data["Strength"].Value;
            playerStats[2].text = "Intellect: " + result.Data["Intellect"].Value;
            playerStats[3].text = "Dexterity: " + result.Data["Dexterity"].Value;
            playerStats[4].text = "Vitality: " + result.Data["Vitality"].Value;
            playerStats[5].text = "Critical Chance: " + result.Data["Critical Chance"].Value;

        }, (error) =>
        {
            Debug.Log("Data request failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });

        playerStats[0].transform.parent.gameObject.SetActive(!playerStats[0].transform.parent.gameObject.activeInHierarchy);
    }
}