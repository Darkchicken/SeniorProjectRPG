using UnityEngine;
using System.Collections;

public class PlayerCombatManager : MonoBehaviour
{

    public GameObject targetEnemy;

    private Animator playerAnimation;
    private PlayerMovement playerMovement;
    private int skillAttackRange;
    private string actionBarSkillId;

    private float skill_1_Timer = 0f;
    private int resource = 0;
    private PlayerBaseSkills playerBaseSkills;



    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerBaseSkills = GetComponent<PlayerBaseSkills>();
    }

    void Update()
    {
        /*
        //return if not local player
        if (!isLocalPlayer)
        { return; }
        */

        targetEnemy = playerMovement.targetEnemy;
        skill_1_Timer += Time.deltaTime;
       
        if (Input.GetMouseButtonDown(0) || actionBarSkillId == "LC")
        {
            skillAttackRange = 2;
            if (InRangeForAttack() && skill_1_Timer >= 0.7f && !targetEnemy.GetComponent<EnemyHealth>().isEnemyDead())
            {
                playerBaseSkills.PrimarySkill(targetEnemy, 0);
                actionBarSkillId = null;
                playerAnimation.SetTrigger("ATTACK 1");
                skill_1_Timer = 0f;
            }
        }
        if(Input.GetMouseButtonDown(1) || actionBarSkillId == "RC")
        {
            if(resource >= 30)
            {
                playerBaseSkills.PrimarySkill(targetEnemy, 1);
                actionBarSkillId = null;
                playerAnimation.SetTrigger("ATTACK 2");
            }
            
        }
        if (Input.GetKeyDown("1") || actionBarSkillId == "1")
        {
            actionBarSkillId = null;
            playerAnimation.SetTrigger("ATTACK 1");

            
        }
        if(Input.GetKeyDown("2") || actionBarSkillId == "2")
        {
            actionBarSkillId = null;
            playerAnimation.SetTrigger("ATTACK 2");
        }
        if(Input.GetKeyDown("3") || actionBarSkillId == "3")
        {
            actionBarSkillId = null;
            playerAnimation.SetTrigger("ATTACK 3");
        }

    }

    bool InRangeForAttack()
    {
        if(targetEnemy != null)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= skillAttackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
        
    }

    public void OnButtonClick(string id)
    {

        
        actionBarSkillId = id;
    }
    /*
    [Command]
    void CmdHitPlayer(GameObject hit)
    {

        hit.GetComponent<NetworkPlayerScript>().RpcResolveHit();
    }
    */

    public int GetPlayerResource()
    {
        return resource;
    }

    public void SetPlayerResource(int generate)
    {
        resource += generate;
    }
}
