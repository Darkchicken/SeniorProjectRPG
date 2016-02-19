using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class CatalogRune
{
    public static CatalogRune rune;

    public string itemId;
    public string itemClass;
    public string displayName;
    public string description;
    public string skillSlot;
    public string resourceGeneration;
    public string resourceUsage;
    public string attackRange;
    public string attackRadius;
    public string cooldown;

    void Awake()
    {
        rune = this;
    }

    public CatalogRune(string _itemId, string _itemClass, string _displayName, string _description, string _skillSlot, string _resourceGeneration, string _resourceUsage, string _attackRange, string _attackRadius, string _cooldown)
    {
        itemId = _itemId;
        itemClass = _itemClass;
        displayName = _displayName;
        description = _description;
        skillSlot = _skillSlot;
        resourceGeneration = _resourceGeneration;
        resourceUsage = _resourceUsage;
        attackRange = _attackRange;
        attackRadius = _attackRadius;
        cooldown = _cooldown;
    }
}
