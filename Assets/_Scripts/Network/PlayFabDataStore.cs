using UnityEngine;
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


    void Awake()
    {
        PlayFabSettings.TitleId = titleId;
    }
}