using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : Runes
{
    //public static PlayerAttack playerAttack;
    public static Dictionary<string, List<Rune>> classRunes;
    public static Dictionary<int, Rune> playerActiveSkillRunes;
    public static string playerClassID = "";

    private PlayerCombatManager playerCombatManager;

    private List<Rune> playerRunes;


    void Start()
    {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        classRunes = new Dictionary<string, List<Rune>>();
        playerActiveSkillRunes = new Dictionary<int, Rune>();
        classRunes.Add("Warrior", warriorRunes);
        Invoke("SetActiveRunes", 0.1f);

    }

    void Update()
    {

    }

    void SetActiveRunes()
    {
        PlayerCombatManager.playerCombatManager.SetActiveSkillRune(warriorRunes[0], 5); // test purpose
        PlayerCombatManager.playerCombatManager.SetActiveSkillRune(warriorRunes[1], 6); // test purpose
    }



    public void PrimarySkill()
    {
        Invoke(playerActiveSkillRunes[5].name, 0);
    }

    public void SecondarySkill()
    {
        Invoke(playerActiveSkillRunes[6].name, 0);
    }

    public void BarSkill_1()
    {

    }

    public void BarSkill_2()
    {

    }

    public void BarSkill_3()
    {

    }

    public void BarSkill_4()
    {

    }
}
