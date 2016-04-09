using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabDataStore : MonoBehaviour
{
    public static string sessionTicket;
    public static string playFabId;
    public static string characterId;
    public static string characterName;
    public static string friendUsername;
    public static string friendCharacterId;
    public static string currentRoomName;
    public static string friendsCurrentRoomName;
    public static int playerCurrency;
    public static int playerInventorySlotCount = 40;

    //Dictionaries

    public static Dictionary<string, string> friendsList = new Dictionary<string, string>();
    public static Dictionary<string, CatalogRune> catalogRunes = new Dictionary<string, CatalogRune>();
    public static Dictionary<string, PlayerRune> playerAllRunes = new Dictionary<string, PlayerRune>();
    public static Dictionary<string, Sprite> catalogRuneImages = new Dictionary<string, Sprite>();
    public static Dictionary<int, Sprite> playerActiveRuneImages = new Dictionary<int, Sprite>();
    public static Dictionary<string, List<PlayerItemInfo>> playerInventoryInfo = new Dictionary<string, List<PlayerItemInfo>>();
    public static Dictionary<string, string> characters = new Dictionary<string, string>();
    public static Dictionary<int, string> playerActiveSkillRunes = new Dictionary<int, string>();
    public static Dictionary<string, int> playerActiveModifierRunes = new Dictionary<string, int>();
    public static Dictionary<string, PlayerItemInfo> playerEquippedItems = new Dictionary<string, PlayerItemInfo>();
    
    public static Dictionary<string, CatalogQuest> catalogQuests = new Dictionary<string, CatalogQuest>();
    public static Dictionary<string, UIItemInfo> catalogItems = new Dictionary<string, UIItemInfo>();

    public static List<string> playerCompletedQuests = new List<string>();
    public static List<string> playerQuestLog = new List<string>();
    public static List<string> playerInventory = new List<string>();
    

    //Player

    public static int playerLevel;
    public static int playerExperience;
    public static int playerBaseHealth;
    public static int playerCurrentHealth;
    public static int playerMaxHealth = 1;
    public static int playerCurrentResource;
    public static int playerMaxResource = 1;
    public static int playerBaseStrength;
    public static int playerStrength;
    public static int playerBaseIntellect;
    public static int playerIntellect;
    public static int playerBaseDexterity;
    public static int playerDexterity;
    public static int playerBaseVitality;
    public static int playerVitality;
    public static int playerBaseSpirit;
    public static int playerSpirit;
    public static int playerBaseCriticalChance;
    public static int playerCriticalChance;
    public static int playerArmor;
    //public static int playerBaseWeaponDamage;
    public static int playerWeaponDamage;
    public static int playerSpellPower;
    public static int playerAttackPower;
    public static Dictionary<string, string> playerInitialData = new Dictionary<string, string>()
    {
        {"Level", "1" },
        {"Experience", "0" },
        {"Health", "55" },
        {"MaxHealth", "55" },
        {"Resource", "100" },
        {"Strength", "0" },
        {"Intellect", "0" },
        {"Dexterity", "0" },
        {"Vitality", "0" },
        {"Spirit", "0" },
        {"Armor", "0" },
        {"Critical Chance", "0" },
        {"Weapon Damage", "0" }
    };

    
    
    
    
    
    

}