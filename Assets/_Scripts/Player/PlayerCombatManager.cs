using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatManager : PlayerAttack
{

    public static GameObject targetEnemy;
    public static PlayerCombatManager playerCombatManager;
    public bool canMove = true; // UI clicks prevent player from moving
    public bool isInCombat = false;
    public float stopDistanceForAttack = 2f; //sets by PlayerAttack script

    private Vector3 position;
    private NavMeshAgent controller;
    private Animator playerAnimation;
    private RaycastHit hit;
    private int skillAttackRange;
    private string actionBarSkillId;

    private float skill_1_Timer = 0f;
    private int resource = 0;
    private float idleTimer = 0f;
    private bool isMoving = false;
    private int skillSlot = 0;
    private bool autoAttack = false;


    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        controller = GetComponent<NavMeshAgent>();
        position = transform.position;
        playerCombatManager = this;
        
    }

    

    void Update()
    {
        //Primary Skill
        if ((Input.GetMouseButtonDown(0) || actionBarSkillId == "LC") && canMove)
        {
            locatePosition(); //Find the clicked position and check if enemy clicked
            skillSlot = 5;
            stopDistanceForAttack = playerActiveSkillRunes[skillSlot].attackRange;
            PrimarySkill();

        }

        //Secondary Skill
        if ((Input.GetMouseButtonDown(1) || actionBarSkillId == "RC") && canMove)
        {
            skillSlot = 6;
            if (resource >= playerActiveSkillRunes[skillSlot].resourceUsage)
            {
                controller.Stop();
                controller.ResetPath();
                stopDistanceForAttack = 2f;
                SecondarySkill();
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
        
        if(isMoving && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.04f)
            {
                playerAnimation.SetBool("IsMoving", false);
                isMoving = false;
                idleTimer = 0f;
            }
        }

        //PlayerMovement Update
        /*if (isMoving && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.04f)
            {
                isMoving = false;
                targetEnemy = null;
                playerAnimation.SetBool("IsMoving", false);
                if (isInCombat)
                {
                    playerAnimation.SetTrigger("IDLE WEAPON");
                }
                else
                {
                    playerAnimation.SetTrigger("IDLE");
                }
                idleTimer = 0f;
            }
        }*/

        if (targetEnemy != null && isMoving)
        {
            if (targetEnemy.CompareTag("Enemy"))
            {
                position = targetEnemy.transform.position;
                MoveToPosition();
            }
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
                playerAnimation.SetTrigger("RUN");
                isMoving = true;
                playerAnimation.SetBool("IsMoving", true);
            }

        }
    }

    public void SetActiveSkillRune(Rune activeRune, int skillSlot)
    {
        playerActiveSkillRunes.Add(skillSlot, activeRune);
    }

    public void OnButtonClick(string id)
    {
        actionBarSkillId = id;
    }

    public int GetPlayerResource()
    {
        return resource;
    }

    public void SetPlayerResource(int generate)
    {
        resource += generate;
    }
}
