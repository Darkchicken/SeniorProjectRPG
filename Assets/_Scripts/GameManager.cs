using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas loading;
    public Canvas runes;
    public Canvas character;
    public UIItemDatabase itemDatabase;
    public static List<GameObject> players = new List<GameObject>();

    //This is where we call all our Database when loading a scene
	void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        itemDatabase.items.Clear();
        loading.gameObject.SetActive(true);
        runes.gameObject.SetActive(true);
        PlayFabApiCalls.GetAllCharacterRunes();
        PlayFabApiCalls.GetAllCharacterItems();
        PlayFabApiCalls.GetCharacterCompletedQuests();
        PlayFabApiCalls.GetCharacterStats();
        PlayFabApiCalls.GetFriendsList();
        PlayFabApiCalls.GetQuestLog();

        //Invoke("CalculateStats", 1);
        
        
        Invoke("SetPlayerHealth", 2);
        Invoke("RefreshActionBar", 3);
        Invoke("UpdatePlayerHealth", 5);
    }

    void CalculateStats()
    {
        CharacterStats.characterStats.CalculateStats();
    }

    void SortRunes()
    {
        Debug.Log("RunesSorted");
        RuneWindow.SortAllRunes();

    }

    void SetPlayerHealth()
    {
        runes.gameObject.SetActive(false);
        PlayFabDataStore.playerMaxHealth = PlayFabDataStore.playerBaseHealth;
        PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
    }

    void RefreshActionBar()
    {
        character.gameObject.SetActive(true);
        ActionBar.RefreshActionBar();
        Debug.Log("RefreshedActionBar");
        RuneWindow.ToggleWindows();
        

    }

    void UpdatePlayerHealth()
    {
        PlayFabDataStore.playerMaxHealth = PlayFabDataStore.playerBaseHealth;
        character.gameObject.SetActive(false);
        character.gameObject.SetActive(true);
        character.gameObject.SetActive(false);
        PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
    }

    

    public void CalculatePlayerStats()
    {

    }

    
}
