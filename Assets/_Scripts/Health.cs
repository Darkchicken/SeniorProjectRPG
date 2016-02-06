using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Slider enemyHealthSlider;
    public int health;

    private Animator anim;
    private bool dead = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(GameObject source , int damage)
    {
        if (!dead)
        {
            if (health > damage)
            {
                anim.SetTrigger("TAKE DAMAGE 1");
                health -= damage;
            }
            else
            {
                Dead();
                if (tag == "Player")
                {
                    if(source.GetComponent<EnemyCombatManager>().playerAttackList.Contains(gameObject))
                    {
                        source.GetComponent<EnemyCombatManager>().playerAttackList.Remove(gameObject);
                    }
                }
            }
        }

    }

    void Dead()
    {
        dead = true;
        anim.SetTrigger("DIE");
        if(tag == "Player")
        {
            GetComponent<PlayerCombatManager>().enabled = false;
        }
        if(tag == "Enemy")
        {
            enemyHealthSlider.gameObject.SetActive(false);
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyCombatManager>().enabled = false;
        }
        
        GetComponent<NavMeshAgent>().enabled = false; 
        GetComponent<Health>().enabled = false;       
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void OnMouseOver()
    {
        if (tag == "Enemy")
        {
            if (!dead)
            {
                enemyHealthSlider.value = health;
                enemyHealthSlider.gameObject.SetActive(true);
            }
        }

    }

    void OnMouseExit()
    {
        if (tag == "Enemy")
        {
            enemyHealthSlider.gameObject.SetActive(false);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        return dead;
    }
}
