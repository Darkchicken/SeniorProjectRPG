using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public Canvas loading;
    public Canvas runes;
    public static List<GameObject> players = new List<GameObject>();

    //This is where we call all our Database when loading a scene
	void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        loading.gameObject.SetActive(true);
        PlayFabApiCalls.GetAllCharacterRunes();
        PlayFabApiCalls.GetCharacterData();

        Invoke("SortRunes", 1);
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
        runes.gameObject.SetActive(false);
    }
}
