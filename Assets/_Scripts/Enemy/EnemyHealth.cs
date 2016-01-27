using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth :MonoBehaviour {

    public Slider enemyHealthSlider;
    public int enemyHealth;

    private Animator enemyAnimation;
    private GameObject player;

    private bool dead = false;

    void Awake ()
    {
        enemyAnimation = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

	}

	void Update ()
    {

	}

    void OnMouseOver()
    {
        if(!dead)
        {
            enemyHealthSlider.value = enemyHealth;
            enemyHealthSlider.gameObject.SetActive(true);
            if (player != null)
            {
                //player.GetComponent<PlayerCombatManager>().targetEnemy = gameObject;
                PlayerCombatManager.targetEnemy = gameObject;
            }
        }
        
    }

    void OnMouseExit()
    {
        enemyHealthSlider.gameObject.SetActive(false);
        if (player != null)
        {
            //player.GetComponent<PlayerCombatManager>().targetEnemy = null;
            PlayerCombatManager.targetEnemy = null;
        }
    }

    public void TakeDamage(int damage)
    {
        if(!dead)
        {
            if (enemyHealth > damage)
            {
                enemyAnimation.SetTrigger("TAKE DAMAGE 1");
                enemyHealth -= damage;
            }
            else
            {              
                EnemyDead();
            }
        }
               
    }

    void EnemyDead()
    {
        dead = true;
        enemyAnimation.SetTrigger("DIE");
        enemyHealthSlider.gameObject.SetActive(false);    
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponent<EnemyCombatManager>().enabled = false;
    }

    public bool isEnemyDead()
    {
        return dead;
    }

}
