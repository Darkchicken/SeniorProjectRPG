using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public static CharacterStats characterStats;

    public Text textStrength;
    public Text textDexterity;
    public Text textIntellect;
    public Text textVitality;
    public Text textSpirit;
    public Text textCrit;
    public Text textArmor;
    public Text textPhysicalDamage;
    public Text textSpellPower;
    public Text textAttackPower;
    public Text textCurrency;

    private float calculationTimer = 0f;
    private float calculationTimerMax = 1f;

    void Awake()
    {
        characterStats = this;
    }

    void Start()
    {
        Invoke("CalculateStats", 2);
        
    }

    void OnEnable()
    {
        //SetStatsText();
    }

    void Update()
    {
        calculationTimer += Time.deltaTime;
    }

    public void SetStatsText()
    {
        textStrength.text = PlayFabDataStore.playerStrength.ToString();
        textDexterity.text = PlayFabDataStore.playerDexterity.ToString();
        textIntellect.text = PlayFabDataStore.playerIntellect.ToString();
        textVitality.text = PlayFabDataStore.playerVitality.ToString();
        textSpirit.text = PlayFabDataStore.playerSpirit.ToString();
        textArmor.text = PlayFabDataStore.playerArmor.ToString();
        textPhysicalDamage.text = PlayFabDataStore.playerWeaponDamage.ToString();
        textSpellPower.text = PlayFabDataStore.playerSpellDamage.ToString();
        textAttackPower.text =  PlayFabDataStore.playerAttackPower.ToString();
        textCrit.text = PlayFabDataStore.playerCriticalChance.ToString() + "%";
        textCurrency.text = PlayFabDataStore.playerCurrency.ToString();
    }

    public void CalculateStats()
    {
        if(calculationTimer > calculationTimerMax)
        {
            calculationTimer = 0;
            Debug.Log("Calculating the Stats...");
            CalculateWeaponDamage();
            CalculateVitality();
            CalculateStrength();
            CalculateDexterity();
            CalculateIntellect();
            CalculateSpirit();
            CalculateCriticalChance();
            CalculateArmor();
            CalculatePhysicalDamage();
            CalculateSpellPower();
            CalculateAttackPower();
            textCurrency.text = PlayFabDataStore.playerCurrency.ToString();
            Invoke("UpdateHealth", 0);
        }

    }

    void UpdateHealth()
    {
        GameManager.players[0].GetComponent<Health>().CharacterStatsUpdateHealth(PlayFabDataStore.playerMaxHealth);
    }

    void CalculateVitality()
    {
        int vitality = 0;
        foreach(var item in PlayFabDataStore.playerEquippedItems)
        {
            vitality += PlayFabDataStore.catalogItems[item.Value.itemId].vitality;
        }

        PlayFabDataStore.playerVitality = PlayFabDataStore.playerBaseVitality + PlayFabDataStore.statsBuilder["Vitality"] + vitality;
        PlayFabDataStore.playerMaxHealth = PlayFabDataStore.playerBaseHealth + PlayFabDataStore.playerVitality * 10;
        textVitality.text = PlayFabDataStore.playerVitality.ToString();
        Debug.Log("Health Max: " + PlayFabDataStore.playerMaxHealth);
        if(PlayFabDataStore.playerMaxHealth < PlayFabDataStore.playerCurrentHealth)
        {
            PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
        }
    }
    void CalculateStrength()
    {
        int strength = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            strength += PlayFabDataStore.catalogItems[item.Value.itemId].strength;
        }

        PlayFabDataStore.playerStrength = PlayFabDataStore.playerBaseStrength + PlayFabDataStore.statsBuilder["Strength"] + strength;
        textStrength.text = PlayFabDataStore.playerStrength.ToString();
    }
    void CalculateDexterity()
    {
        int dexterity = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            dexterity += PlayFabDataStore.catalogItems[item.Value.itemId].dexterity;
        }

        PlayFabDataStore.playerDexterity = PlayFabDataStore.playerBaseDexterity + PlayFabDataStore.statsBuilder["Dexterity"] + dexterity;
        textDexterity.text = PlayFabDataStore.playerDexterity.ToString();
    }
    void CalculateIntellect()
    {
        int intellect = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            intellect += PlayFabDataStore.catalogItems[item.Value.itemId].intellect;
        }

        PlayFabDataStore.playerIntellect = PlayFabDataStore.playerBaseIntellect + PlayFabDataStore.statsBuilder["Intellect"] + intellect;
        textIntellect.text = PlayFabDataStore.playerIntellect.ToString();
    }
    void CalculateSpirit()
    {
        int spirit = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            spirit += PlayFabDataStore.catalogItems[item.Value.itemId].spirit;
        }

        PlayFabDataStore.playerSpirit = PlayFabDataStore.playerBaseSpirit + PlayFabDataStore.statsBuilder["Spirit"] + spirit;
        textSpirit.text = PlayFabDataStore.playerSpirit.ToString();
    }
    public void CalculateCriticalChance()
    {
        int crit = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            crit += PlayFabDataStore.catalogItems[item.Value.itemId].crit;
        }

        PlayFabDataStore.playerCriticalChance = PlayFabDataStore.playerBaseCriticalChance + PlayFabDataStore.statsBuilder["CriticalChance"] + crit / 100;
        textCrit.text = PlayFabDataStore.playerCriticalChance.ToString() + "%";
    }
    void CalculateArmor()
    {
        int armor = 0;
        int strength = 0;
        int dexterity = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            armor += PlayFabDataStore.catalogItems[item.Value.itemId].armor;
            strength += PlayFabDataStore.catalogItems[item.Value.itemId].strength;
            dexterity += PlayFabDataStore.catalogItems[item.Value.itemId].dexterity;
        }

        PlayFabDataStore.playerArmor = PlayFabDataStore.playerBaseArmor + armor + strength + dexterity;
        textArmor.text = PlayFabDataStore.playerArmor.ToString();
    }

    void CalculateWeaponDamage()
    {
        int damage = 0;
        foreach (var item in PlayFabDataStore.playerEquippedItems)
        {
            Debug.Log("DAMAGEEE " + damage);
            damage += PlayFabDataStore.catalogItems[item.Value.itemId].damage;
        }

        PlayFabDataStore.playerWeaponDamage = PlayFabDataStore.playerBaseWeaponDamage + damage;
    }
    void CalculatePhysicalDamage()
    {
        PlayFabDataStore.playerPhysicalDamage = PlayFabDataStore.playerWeaponDamage + (PlayFabDataStore.playerWeaponDamage * PlayFabDataStore.playerStrength / 100);

        textPhysicalDamage.text = PlayFabDataStore.playerWeaponDamage.ToString();
    }
    void CalculateSpellPower()
    {
        PlayFabDataStore.playerSpellDamage = PlayFabDataStore.playerWeaponDamage + (PlayFabDataStore.playerWeaponDamage * PlayFabDataStore.playerIntellect / 100);

        textSpellPower.text = PlayFabDataStore.playerSpellDamage.ToString();
    }
    void CalculateAttackPower()
    {
        PlayFabDataStore.playerAttackPower = PlayFabDataStore.playerWeaponDamage + (PlayFabDataStore.playerWeaponDamage * PlayFabDataStore.playerDexterity / 100);

        textAttackPower.text = PlayFabDataStore.playerAttackPower.ToString();
    }

    
}
