using UnityEngine;
using System.Collections;

public class EnemyMovement :MonoBehaviour
{

    public float aggroRange = 15f;
    public float chasingRange = 50f;
    public float chaseStopDistance = 2f;

    private NavMeshAgent controller;
    private Animator enemyAnimation;
    private GameObject player;
    private Vector3 initialPosition;


    private bool isChasing = false;
    private bool immuneToAggro = false;
    private float idleTimer = 0f;
    private bool isMoving_Animation = false;
    private bool isInCombat = false;
    private bool isTargetDead = false;


    void Start()
    {
        controller = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        controller.stoppingDistance = chaseStopDistance;
    }

    void Update()
    {
        //if a player was not found initially, keep looking until one is found
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            if ((InAggroRange() || isChasing) && !immuneToAggro && !InAttackingRange() && !player.GetComponent<PlayerHealth>().IsPlayerDead())
            {
                MoveToPosition(player.transform.position);
                controller.stoppingDistance = chaseStopDistance;

                if (!InChasingRange())
                {
                    MoveToPosition(initialPosition);
                }
            }

            if (!isTargetDead && player.GetComponent<PlayerHealth>().IsPlayerDead())
            {
                isTargetDead = true;
                immuneToAggro = true;
                isInCombat = false;
                player = null;
                controller.stoppingDistance = 0;
                MoveToPosition(initialPosition);
            }

            
            if (!immuneToAggro && isChasing)
            {
                transform.LookAt(player.transform.position);
            }
        }

        if (isMoving_Animation && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.05f)
            {
                isMoving_Animation = false;
                enemyAnimation.SetBool("IsMoving", false);
                if (isInCombat)
                {
                    enemyAnimation.SetTrigger("IDLE WEAPON");
                }
                else
                {
                    enemyAnimation.SetTrigger("IDLE");
                }

                idleTimer = 0f;
                immuneToAggro = false;
            }
        }

    }

    bool InAggroRange() // returns true if player in distance
    {
        //if no player is found
        if(player == null)
        {
            return false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= aggroRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool InChasingRange() // returns true if enemy is not too far from its initial location
    {
        if (Vector3.Distance(initialPosition, transform.position) > chasingRange)
        {
            immuneToAggro = true;
            isChasing = false;
            controller.stoppingDistance = 0;
            player.GetComponent<PlayerMovement>().isInCombat = false;
            isInCombat = false;
            return false;
        }
        else
        {
            return true;
        }
    }

    bool InAttackingRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseStopDistance)
        {
            player.GetComponent<PlayerMovement>().isInCombat = true;
            isInCombat = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveToPosition(Vector3 position)
    {
        
        transform.LookAt(position);
        controller.SetDestination(position);


        if (!isMoving_Animation)
        {
            isChasing = true;
            isMoving_Animation = true;
            enemyAnimation.SetBool("IsMoving", true);
            enemyAnimation.SetTrigger("RUN");

        }
    }

    public GameObject targetPlayer()
    {
        return player;
    }


}
