using UnityEngine;
using System.Collections;

public class EnemyCombatManager : MonoBehaviour
{
    public int attackRange;
    public int attackDamage;
    public float attackSpeed;

    private GameObject player;
    private Animator enemyAnimation;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed && !player.GetComponent<PlayerHealth>().IsPlayerDead() && InAttackingRange())
        {
            if(player != null)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                enemyAnimation.SetTrigger("ATTACK 1");
                attackTimer = 0f;
            }
            
        }
    }

    bool InAttackingRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
