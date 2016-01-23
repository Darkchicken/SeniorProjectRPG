using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float stopDistanceForAttack = 2f;
    public bool canMove = true;

    private Vector3 position;
    private NavMeshAgent controller;
    private Animator playerAnimation;
    private RaycastHit hit;

    private float idleTimer = 0f;
    private bool isMoving = false;

    void Awake()
    {

        controller = GetComponent<NavMeshAgent>();
        playerAnimation = GetComponent<Animator>();
        position = transform.position;
    }

    void Update()
    {

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && canMove)
        {
            locatePosition();

        }


        if (isMoving && controller.velocity == Vector3.zero)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.04f)
            {
                isMoving = false;
                playerAnimation.SetBool("IsMoving", false);
                playerAnimation.SetTrigger("IDLE");
                idleTimer = 0f;
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
                position = hit.transform.position;
                controller.stoppingDistance = stopDistanceForAttack;
            }
            else
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
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
