using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
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
        if ((InAggroRange() || isChasing) && !immuneToAggro && !InAttackingRange())
        {
            MoveToPosition(player.transform.position);
            controller.stoppingDistance = chaseStopDistance;

            if (!InChasingRange())
            {
                MoveToPosition(initialPosition);
            }
        }

        if (isMoving_Animation && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.05f)
            {
                isMoving_Animation = false;
                enemyAnimation.SetBool("IsMoving", false);
                enemyAnimation.SetTrigger("IDLE");
                idleTimer = 0f;
                immuneToAggro = false;
            }
        }
        if (!immuneToAggro && isChasing)
        {
            transform.LookAt(player.transform.position);
        }

    }

    bool InAggroRange() // returns true if player in distance
    {
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


}
