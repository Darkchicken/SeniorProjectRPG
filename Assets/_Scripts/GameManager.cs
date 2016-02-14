using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public Canvas loading;
    public static List<GameObject> players = new List<GameObject>();

    //This is where we call all our Database when loading a scene
	void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        loading.gameObject.SetActive(true);
        PlayFabApiCalls.GetAllCharacterRunes();
        PlayFabApiCalls.GetCharacterData();
    }
}
