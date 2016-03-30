using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public Text textStrength;
    public Text textDexterity;
    public Text textIntellect;
    public Text textVitality;
    public Text textSpirit;
    public Text textCrit;
    public Text textArmor;
    public Text textWeaponDamage;

    void OnEnable()
    {
        textStrength.text = PlayFabDataStore.playerStrength.ToString();
        textDexterity.text = PlayFabDataStore.playerDexterity.ToString();
        textIntellect.text = PlayFabDataStore.playerIntellect.ToString();
        textVitality.text = PlayFabDataStore.playerVitality.ToString();
        textSpirit.text = PlayFabDataStore.playerSpirit.ToString();
        textCrit.text = PlayFabDataStore.playerCriticalChance.ToString();
        textArmor.text = PlayFabDataStore.playerArmor.ToString();
        textWeaponDamage.text = PlayFabDataStore.playerWeaponDamage.ToString();
    }
}
