using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCombatManager : NetworkBehaviour
{

    public GameObject targetEnemy;

    private Animator playerAnimation;
    private int skillAttackRange;
    private string actionBarSkillId;

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        //targetEnemy = gameObject;
    }

    void Update()
    {
        //return if not local player
        if (!isLocalPlayer)
        { return; }

       
        if (Input.GetMouseButtonDown(0) || actionBarSkillId == "LC")
        {
            skillAttackRange = 2;
            if (InRangeForAttack())
            {
                actionBarSkillId = null;
                playerAnimation.SetTrigger("ATTACK 1");
            }
        }
        if(Input.GetMouseButtonDown(1) || actionBarSkillId == "RC")
        {
            actionBarSkillId = null;
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

    [Command]
    void CmdHitPlayer(GameObject hit)
    {

        hit.GetComponent<NetworkPlayerScript>().RpcResolveHit();
    }
}
