﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{

    public float stopDistanceForAttack = 2f;
    public bool canMove = true;
    public bool isInCombat = false;

    private Vector3 position;
    private NavMeshAgent controller;
    private Animator playerAnimation;
    private RaycastHit hit;

    private float idleTimer = 0f;
    private bool isMoving = false;
    private GameObject targetEnemy;

    void Awake()
    {

        controller = GetComponent<NavMeshAgent>();
        playerAnimation = GetComponent<Animator>();
        position = transform.position;
       
    }
    void Start()
    {
       
    }

    void Update()
    {
        //return if not local player
        if(!isLocalPlayer)
        {return;}


        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && canMove)
        {
            locatePosition();

        }


        if (isMoving && controller.velocity == Vector3.zero)
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
        }

        if (targetEnemy != null)
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
            if(hit.transform.tag == "Enemy" && Vector3.Distance(hit.transform.position, transform.position) <= stopDistanceForAttack)
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
}
