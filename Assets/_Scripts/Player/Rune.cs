using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class Rune
{
    public static Rune rune;

    public string itemId;
    public string instanceId;
    public string itemClass;
    public string displayName;
    public string description;
    public string active;

    void Awake()
    {
        rune = this;
    }

    public Rune(string _itemId, string _instanceId, string _itemClass, string _displayName, string _active)
    {
        itemId = _itemId;
        instanceId =_instanceId;
        itemClass = _itemClass;
        displayName = _displayName;
        active = _active;

    }
}
