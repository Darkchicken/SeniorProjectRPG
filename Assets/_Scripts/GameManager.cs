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
    public static List<GameObject> players = new List<GameObject>();

    //This is where we call all our Database when loading a scene
	void Awake()
    {
        loading.gameObject.SetActive(true);
        runes.gameObject.SetActive(true);
        PlayFabApiCalls.GetAllCharacterRunes();
        PlayFabApiCalls.GetCharacterData();
        PlayFabApiCalls.GetFriendsList();

        Invoke("SortRunes", 1);
        Invoke("SetPlayerData", 1.5f);
        Invoke("RefreshActionBar", 2);
        
    }

   

    

    void SortRunes()
    {
        Debug.Log("RunesSorted");
        RuneWindow.SortAllRunes();

    }
    void RefreshActionBar()
    {
        ActionBar.RefreshActionBar();
        Debug.Log("RefreshedActionBar");
        RuneWindow.ToggleWindows();
    }

    void SetPlayerData()
    {
        runes.gameObject.SetActive(false);
        PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
    }

    
}
