using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> players = new List<GameObject>();

	void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void SortRunes()
    {
        foreach(var rune in PlayFabDataStore.playerAllRunes)
        {
            if(rune.itemClass == "Skill")
            {
            }
            if(rune.itemClass == "Modifier")
            {

            }
        }
        
    }
}
