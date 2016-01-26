using UnityEngine;
using System.Collections;

public class PlayerBaseSkills : MonoBehaviour {

    private PlayerCombatManager playerCombatManager;
    private PrimarySkillAreaDamageSkill areaDamage;


    //Primary Skill Set
    private string primarySkill_ID = "Skill_1";
    private string primarySkill_Name = "Primary";
    private int primarySkill_ResourceGeneration = 15;
    private int primarySkill_AttackDamage = 10;
    private float primarySkill_Cooldown = 0f;
    private int primarySkill_AttackRange = 3;

    //Secondary SKill Set
    private string secondarySkill_ID = "Skill_2";
    private string secondarySkill_Name = "Secondary";
    private int secondarySkill_ResourceUsage = 25;
    private int secondarySkill_AttackDamage = 40;
    private float secondarySkill_Cooldown = 0f;
    private int secondarySkill_AttackRange = 0;


    void Start()
    {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        areaDamage = GetComponent<PrimarySkillAreaDamageSkill>();
    }

    public void PrimarySkill(GameObject target, int id)
    {
        if(id == 0)
        {    
            target.GetComponent<EnemyHealth>().TakeDamage(primarySkill_AttackDamage);
            if(playerCombatManager.GetPlayerResource() + primarySkill_ResourceGeneration <= 100 )
            {
                playerCombatManager.SetPlayerResource(primarySkill_ResourceGeneration);
            }
            else
            {
                playerCombatManager.SetPlayerResource(100);
            }
            
        }
        if(id == 1)
        {
            if(playerCombatManager.GetPlayerResource() >= secondarySkill_ResourceUsage)
            {
                areaDamage.PrimarySkillAreaDamage();
                playerCombatManager.SetPlayerResource(- secondarySkill_ResourceUsage);
            }
            
        }
        
    }
}
