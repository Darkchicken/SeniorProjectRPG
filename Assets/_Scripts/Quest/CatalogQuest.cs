using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class CatalogQuest
{
    public static CatalogQuest quest;

    public string itemId;
    public string itemClass;
    public string displayName;
    public string description;
    public string requirements;
    public List<string> rewards;
    

    void Awake()
    {
        quest = this;
    }

    public CatalogQuest(string _itemId, string _itemClass, string _displayName, string _description, string _requirements, List<string> _rewards)
    {
        itemId = _itemId;
        itemClass = _itemClass;
        displayName = _displayName;
        description = _description;
        requirements = _requirements;
        rewards = _rewards;
    }
}
