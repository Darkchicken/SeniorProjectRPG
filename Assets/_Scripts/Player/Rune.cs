using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class Rune
{
    public static Rune rune;

    public string itemId;
    public string itemClass;
    public string displayName;
    public string description;
    public string skillSlot;

    void Awake()
    {
        rune = this;
    }

    public Rune(string _itemId, string _itemClass, string _displayName, string _description, string _skillSlot)
    {
        itemId = _itemId;
        itemClass = _itemClass;
        displayName = _displayName;
        description = _description;
        skillSlot = _skillSlot;

    }
}
