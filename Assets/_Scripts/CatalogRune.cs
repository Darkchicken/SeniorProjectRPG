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
    public int resourceGeneration;
    public int resourceUsage;
    public int attackRange;
    public int attackRadius;
    public float cooldown;

    void Awake()
    {
        rune = this;
    }

    public CatalogRune(string _itemId, string _itemClass, string _displayName, string _description, string _skillSlot, int _resourceGeneration, int _resourceUsage, int _attackRange, int _attackRadius, float _cooldown)
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
