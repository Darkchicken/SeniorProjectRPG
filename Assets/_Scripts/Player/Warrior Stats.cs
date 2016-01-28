using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarriorStats : MonoBehaviour 
{
    
    public int Level = 1;
    public int Strength = 10;
    public int Dexterity = 6;
    public int Intelligence = 5;
    public int Vitality = 10;
    public int PlayerHealth;	//remove player health value on PlayerHealth Script; Set inheritence
    public int Damage;
    public int Defense;
    public int TempTotalStr = 0;   // Get variable from PlayerEquipment
    public int TempTotalVit = 0;    // Get variable from PlayerEquipment
    public int TempTotalDef = 0;    // Get variable from PlayerEquipment
    public float BaseExp = 0f;
    public float ExpToLvl = 20f;

    public int Currency = 0;

    void Start()
    {

    }


    void Update()               //constantly checks for any stat changes, exp changes, and modifies stats accordingly
    {
        CheckStr();
        CheckVit();
        DamageMod(Strength);
        HealthMod(Vitality);
        ExpCheck(BaseExp);
    }

    public void DamageMod(int Strength) //modifier to Damage
    {
        int tempStr = Strength % 6;
        Damage = (tempStr * 6) + Strength; //Add weapon damage to formula
        
    }

    public void HealthMod(int Vitality)     //modifier to HP
    {
        int tempVit = Vitality % 7;
        PlayerHealth = (tempVit * 15) + (Vitality * 10);                       // total hp = BonusHP + playerHealth
        
    }

    public void ExpCheck(float BaseExp)     //level up and exp checker
    {
        if (BaseExp == ExpToLvl)
        {
            Level++;
            ExpToLvl = ExpToLvl * 10f;
        }
    }

    public void CheckStr() //add total bonus strength from gear as an argument; adds bonus str from gear to str stat 
    {
        if (TempTotalStr < Strength || TempTotalStr > Strength)  //change Strength in condition to TotalGearStr
        {
            TempTotalStr = Strength - TempTotalStr;
            Strength = Strength + TempTotalStr;
        }
    }

    public void CheckVit()  //add total bonus vit from gear as an argument; adds bonus vit from gear to vit stat
    {
        if (TempTotalVit < Vitality || TempTotalVit > Vitality)  //change Vitality in condition to TotalGearVit
        {
            TempTotalVit = Vitality - TempTotalVit;
            Vitality = Vitality + TempTotalVit;
        }
    }
}