using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : WarriorRunes
{

    public static Dictionary<string, List<Rune>> classRunes;
    public static string playerClassID = "";

    private PlayerCombatManager playerCombatManager;

    private List<Rune> playerRunes;


    void Start()
    {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        classRunes = new Dictionary<string, List<Rune>>();
        classRunes.Add("Warrior", warriorRunes);
    }

    void Update()
    {

    }



    public void PrimarySkill()
    {
        playerRunes = classRunes["Warrior"];

        for (int i = 0; i < playerRunes.Count; i++)
        {
            if (playerRunes[i].skillSlot == 5 && playerRunes[i].type)
            {
                Invoke(playerRunes[i].name, 0);
                break;
            }

        }


    }

    public void SecondarySkill()
    {

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
