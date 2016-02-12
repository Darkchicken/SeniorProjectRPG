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

    public static List<Rune> allRunes = new List<Rune>();
    public static Dictionary<string, string> characters = new Dictionary<string, string>();
    public static Dictionary<string, int> playerSkillRunes = new Dictionary<string, int>();
    public static Dictionary<string, int> playerModifierRunes = new Dictionary<string, int>();
    public static Dictionary<int, string> playerActiveSkillRunes = new Dictionary<int, string>();
    public static Dictionary<string, int> playerActiveModifierRunes = new Dictionary<string, int>();

    //Player

    public static int playerLevel;
    public static int playerExperience;
    public static int playerHealth;
    public static int playerResource;
    public static int playerStrength;
    public static int playerIntellect;
    public static int playerDexterity;
    public static int playerVitality;
    public static int playerCriticalChance;
    public static int playerWeaponDamage;
    public static Dictionary<string, string> playerData = new Dictionary<string, string>()
    {
        {"Level", "1" },
        {"Experience", "1" },
        {"Health", "0" },
        {"Resource", "0" },
        {"Strength", "5" },
        {"Intellect", "5" },
        {"Dexterity", "5" },
        {"Vitality", "5" },
        {"Spirit", "5" },
        {"Armor", "5" },
        {"Critical Chance", "0" },
        {"Weapon Damage", "0" }
    };
}