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
    private EnemyCombatManager combatManager;


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
        combatManager = GetComponent<EnemyCombatManager>();
        //player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        controller.stoppingDistance = chaseStopDistance;
    }

    void Update()
    {
        Invoke("IsPlayersInAggroRange", 1f);

        if (combatManager.playerAttackList.Count != 0)
        {
            if (isInCombat && /*!immuneToAggro && InChasingRange() &&*/ !InAttackingRange())
            {
                MoveToPosition(combatManager.playerAttackList[0].transform.position);
                controller.stoppingDistance = chaseStopDistance;
            }

            if(controller.velocity == Vector3.zero && !isInCombat)
            {
                immuneToAggro = false;
            }
        }

        if ((/*!InChasingRange() || */combatManager.playerAttackList.Count == 0) && isInCombat)
        {
            MoveToPosition(initialPosition);
            controller.stoppingDistance = 0;
            immuneToAggro = true;
            isInCombat = false;
        }

        enemyAnimation.SetFloat("MOVE", controller.velocity.magnitude / controller.speed);

    }

    void IsPlayersInAggroRange()
    {
        for (int i = 0; i < GameManager.players.Count; i++)
        {
            //added this check to prevent null reference exceptions when player leaves room
            if (GameManager.players[i] != null)
            {
                if (Vector3.Distance(transform.position, GameManager.players[i].transform.position) <= aggroRange)
                {
                    if (!combatManager.playerAttackList.Contains(GameManager.players[i]) && !GameManager.players[i].GetComponent<Health>().IsDead())
                    {
                        combatManager.playerAttackList.Add(GameManager.players[i]);
                    }
                }
                if (Vector3.Distance(transform.position, GameManager.players[i].transform.position) > aggroRange)
                {
                    if (combatManager.playerAttackList.Contains(GameManager.players[i]))
                    {
                        combatManager.playerAttackList.Remove(GameManager.players[i]);
                    }
                }
            }
        }
        if (combatManager.playerAttackList.Count > 0)
        {
            isInCombat = true;
        }
    }
    /*
    bool InChasingRange() // returns true if enemy is not too far from its initial location
    {
        if (Vector3.Distance(initialPosition, transform.position) > chasingRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }*/

    bool InAttackingRange()
    {
        if(combatManager.playerAttackList.Count != 0)
        {
            if (Vector3.Distance(transform.position, combatManager.playerAttackList[0].transform.position) <= chaseStopDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }


}
