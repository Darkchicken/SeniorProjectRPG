using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Image enemyHealthFillImage;

    public int health;
    public int maxHealth;

    private Animator anim;
    private bool dead = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
    }

    public void TakeDamage(GameObject source , int damage)
    {
        if (!dead)
        {
            if(tag == "Player")
            {
                if (PlayFabDataStore.playerCurrentHealth > damage)
                {
                    anim.SetTrigger("TAKE DAMAGE 1");
                    PlayFabDataStore.playerCurrentHealth -= damage;
                }
                else
                {
                    Dead();
                    if (source.GetComponent<EnemyCombatManager>() != null)
                    {
                        if (source.GetComponent<EnemyCombatManager>().playerAttackList.Contains(gameObject))
                        {
                            source.GetComponent<EnemyCombatManager>().playerAttackList.Remove(gameObject);
                        }
                    }
                }
            }
            if (tag == "Enemy")
            {
                if (health > damage)
                {
                    anim.SetTrigger("TAKE DAMAGE 1");
                    health -= damage;
                }
                else
                {
                    Dead();
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
            enemyHealthFillImage.transform.parent.gameObject.SetActive(false);
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
                enemyHealthFillImage.fillAmount= (float)health / (float)maxHealth;
                enemyHealthFillImage.transform.parent.gameObject.SetActive(true);
            }
        }

    }

    void OnMouseExit()
    {
        if (tag == "Enemy")
        {
            enemyHealthFillImage.transform.parent.gameObject.SetActive(false);
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
