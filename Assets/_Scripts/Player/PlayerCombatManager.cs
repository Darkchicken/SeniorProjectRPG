using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatManager : Runes
{

    public bool canMove = true; // UI clicks prevent player from moving
    public bool isInCombat = false;
    public bool canAttack = true;
     //sets by PlayerAttack script

    private RaycastHit hit;
    //private int skillAttackRange;
    private string actionBarSkillId;

    private float skill_1_Timer = 0f;
    private int resource = 0;
    private float idleTimer = 0f;
    private bool isMoving = false;
    private int skillSlot = 0;
    private bool autoAttack = false;


    void Update()
    {
        //Primary Skill
        if ((Input.GetMouseButtonDown(0) || actionBarSkillId == "LC") && canMove && canAttack)
        {
            locatePosition(); //Find the clicked position and check if enemy clicked
            skillSlot = 5;
            //isFreezing = false;
            //isStunning = false;
            if(PlayFabDataStore.playerActiveSkillRunes.ContainsKey(skillSlot))
            {
                Invoke(PlayFabDataStore.playerActiveSkillRunes[skillSlot], 0);
                actionBarSkillId = null;
            }
            

        }

        //Secondary Skill
        if ((Input.GetMouseButtonDown(1) || actionBarSkillId == "RC") && canMove && canAttack)
        {
            skillSlot = 6;
            if(PlayFabDataStore.playerActiveSkillRunes.ContainsKey(skillSlot))
            {
                Debug.Log("ATTAK");
                Invoke(PlayFabDataStore.playerActiveSkillRunes[skillSlot], 0);
                actionBarSkillId = null;
            }          
            else
            {
                locatePosition();
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
        /*
        if(isMoving && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.04f)
            {
                playerAnimation.SetBool("IsMoving", false);
                isMoving = false;
                idleTimer = 0f;
            }
        }*/

        playerAnimation.SetFloat("MOVE", controller.velocity.magnitude / controller.speed);

        if (targetEnemy != null && isMoving)
        {
            if (targetEnemy.CompareTag("Enemy"))
            {
                position = targetEnemy.transform.position;
                MoveToPosition();
            }
        }

    }
  
    void locatePosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.tag == "Enemy")
            {
                targetEnemy = hit.transform.gameObject;
                position = hit.transform.position;
                controller.stoppingDistance = stopDistanceForAttack;
                isInCombat = true;
            }
            else
            {
                targetEnemy = null;
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                controller.stoppingDistance = 0f;
            }
        }
        MoveToPosition();
    }


    void MoveToPosition()
    {
        transform.LookAt(position);
        controller.SetDestination(position);

        if (!isMoving)
        {
            if (hit.transform.tag == "Enemy" && Vector3.Distance(hit.transform.position, transform.position) <= stopDistanceForAttack)
            {

            }
            else
            {
                //playerAnimation.SetTrigger("RUN");
                isMoving = true;
                //playerAnimation.SetBool("IsMoving", true);
            }

        }
    }


    public void OnButtonClick(string id)
    {
        actionBarSkillId = id;
    }


}
