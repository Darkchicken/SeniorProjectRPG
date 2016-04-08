﻿using UnityEngine;
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
        DontDestroyOnLoad(gameObject);
        gameObject.name = "GameManager";
        itemDatabase.items.Clear();
        loading.gameObject.SetActive(true);
        runes.gameObject.SetActive(true);
        PlayFabApiCalls.GetAllCharacterRunes();
        PlayFabApiCalls.GetAllCharacterItems();
        PlayFabApiCalls.GetCharacterCompletedQuests();
        PlayFabApiCalls.GetCharacterStats();
        PlayFabApiCalls.GetFriendsList();
        PlayFabApiCalls.GetQuestLog();
        PlayFabApiCalls.GetUserVirtualCurrency();

        //Invoke("CalculateStats", 1);
        
        
        //Invoke("SetPlayerHealth", 2);
        Invoke("RefreshActionBar", 3);
        Invoke("UpdatePlayerHealth", 5);
    }

    void CalculateStats()
    {
        //CharacterStats.characterStats.CalculateStats();
        HUD_Manager.hudManager.GetComponent<CharacterStats>().CalculateStats();
    }

    void SortRunes()
    {
        Debug.Log("RunesSorted");
        RuneWindow.SortAllRunes();

    }

    void SetPlayerHealth()
    {
        //PlayFabDataStore.playerMaxHealth = PlayFabDataStore.playerBaseHealth;
        //PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
    }

    void RefreshActionBar()
    {
        ActionBar.RefreshActionBar();
        Debug.Log("RefreshedActionBar");
        RuneWindow.ToggleWindows();
        runes.gameObject.SetActive(false);
    }

    void UpdatePlayerHealth()
    {
        PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
    }

    void OnLevelWasLoaded(int level)
    {
        players.Clear();
    }


    }
