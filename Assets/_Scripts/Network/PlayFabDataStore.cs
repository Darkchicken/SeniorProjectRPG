using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabDataStore : MonoBehaviour
{
    public string TitleId;
    public static string SessionTicket;
    public static string PlayFabId;
    public static List<CatalogItem> Catalog;
    public static List<StoreItem> Store;
    public static Queue<StartPurchaseResult> Orders = new Queue<StartPurchaseResult>();
    public static string characterID;


    void Awake()
    {
        PlayFabSettings.TitleId = TitleId;
    }
}