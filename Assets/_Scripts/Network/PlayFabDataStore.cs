using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabDataStore : MonoBehaviour
{
    public static string sessionTicket;
    public static string playFabId;
    public static string characterId;
    public Text[] playerStats;
    

    public static Dictionary<string, int> playerSkillRunes = new Dictionary<string, int>();
    public static Dictionary<string, int> playerModifierRunes = new Dictionary<string, int>();
    public static Dictionary<int, string> playerActiveSkillRunes = new Dictionary<int, string>();
    public static Dictionary<string, int> playerActiveModifierRunes = new Dictionary<string, int>(); 

    //Player
    public static int playerLevel;
    public static int playerExperience;
    public static int playerHealth;
    public static int playerResource;
    public static int playerStrength;
    public static int playerIntellect;
    public static int playerDexterity;
    public static int playerVitality;
    public static int playerCriticalChance;
    public static int playerWeaponDamage;
    public static Dictionary<string, string> playerData = new Dictionary<string, string>()
    {
        {"Level", "1" },
        {"Experience", "1" },
        {"Health", "0" },
        {"Resource", "0" },
        {"Strength", "5" },
        {"Intellect", "5" },
        {"Dexterity", "5" },
        {"Vitality", "5" },
        {"Spirit", "5" },
        {"Armor", "5" },
        {"Critical Chance", "0" },
        {"Weapon Damage", "0" }
    };


    public void TogglePlayerStatsPanel()
    {

        playerStats[0].transform.parent.gameObject.SetActive(!playerStats[0].transform.parent.gameObject.activeInHierarchy);
        GetCharacterData();

        /*var cloudRequest = new RunCloudScriptRequest()
            {
                ActionId = "grantItem",
                Params = new {itemId = "Runes_Slam", characterId = characterId}
            };
            PlayFabClientAPI.RunCloudScript(cloudRequest, (result) =>
            {
                Debug.Log("SLAM granted");
            }, (error) =>
            {
                Debug.Log("Item not granted");
                Debug.Log(error.ErrorMessage);
                Debug.Log(error.ErrorDetails);
            });*/
    }

    public void GetCharacterData()
    {
        var request = new GetCharacterDataRequest()
        {
            CharacterId = characterId
        };
        PlayFabClientAPI.GetCharacterData(request, (result) =>
        {
            Debug.Log("Data successfully retrieved!");
            playerLevel = int.Parse(result.Data["Level"].Value);
            playerExperience = int.Parse(result.Data["Experience"].Value);
            playerHealth = int.Parse(result.Data["Health"].Value);
            playerResource = int.Parse(result.Data["Resource"].Value);
            playerStrength = int.Parse(result.Data["Strength"].Value);
            playerIntellect = int.Parse(result.Data["Intellect"].Value);
            playerDexterity = int.Parse(result.Data["Dexterity"].Value);
            playerVitality = int.Parse(result.Data["Vitality"].Value);
            playerCriticalChance = int.Parse(result.Data["Critical Chance"].Value);
            playerWeaponDamage = int.Parse(result.Data["Weapon Damage"].Value);

            playerStats[0].text = "Level: " + playerLevel;
            playerStats[1].text = "Strength: " + playerStrength;
            playerStats[2].text = "Intellect: " + playerIntellect;
            playerStats[3].text = "Dexterity: " + playerDexterity;
            playerStats[4].text = "Vitality: " + playerVitality;
            playerStats[5].text = "Critical Chance: " + playerCriticalChance;

        }, (error) =>
        {
            Debug.Log("Character data request failed!");
            Debug.Log(error.ErrorMessage);
            Debug.Log(error.ErrorDetails);
        });
    }


}