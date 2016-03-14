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

    //Dictionaries

    public static Dictionary<string, string> friendsList = new Dictionary<string, string>();
    public static Dictionary<string, CatalogRune> catalogRunes = new Dictionary<string, CatalogRune>();
    public static Dictionary<string, PlayerRune> playerAllRunes = new Dictionary<string, PlayerRune>();
    public static Dictionary<string, Sprite> catalogRuneImages = new Dictionary<string, Sprite>();
    public static Dictionary<int, Sprite> playerActiveRuneImages = new Dictionary<int, Sprite>();
    public static Dictionary<string, string> characters = new Dictionary<string, string>();
    public static Dictionary<int, string> playerActiveSkillRunes = new Dictionary<int, string>();
    public static Dictionary<string, int> playerActiveModifierRunes = new Dictionary<string, int>();
    public static Dictionary<string, CatalogQuest> catalogQuests = new Dictionary<string, CatalogQuest>();
    public static Dictionary<string, PlayerQuest> playerAllQuests = new Dictionary<string, PlayerQuest>();

    public static List<string> playerActiveQuests = new List<string>();
    public static List<string> playerQuestLog = new List<string>();

    //Player

    public static int playerLevel;
    public static int playerExperience;
    public static int playerCurrentHealth;
    public static int playerMaxHealth = 1;
    public static int playerCurrentResource;
    public static int playerMaxResource = 1;
    public static int playerStrength;
    public static int playerIntellect;
    public static int playerDexterity;
    public static int playerVitality;
    public static int playerCriticalChance;
    public static int playerWeaponDamage;
    public static Dictionary<string, string> playerInitialData = new Dictionary<string, string>()
    {
        {"Level", "1" },
        {"Experience", "1" },
        {"Health", "100" },
        {"MaxHealth", "100" },
        {"Resource", "100" },
        {"Strength", "5" },
        {"Intellect", "5" },
        {"Dexterity", "5" },
        {"Vitality", "5" },
        {"Spirit", "5" },
        {"Armor", "5" },
        {"Critical Chance", "5" },
        {"Weapon Damage", "10" }
    };
}