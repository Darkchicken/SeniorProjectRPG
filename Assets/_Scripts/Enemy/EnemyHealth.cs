using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public Text enemyHealthText;
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
            enemyHealthText.text = "Enemy Health: " + enemyHealth;
            player.GetComponent<PlayerCombatManager>().targetEnemy = gameObject;
        }
        
    }

    void OnMouseExit()
    {
        enemyHealthText.text = "";
        player.GetComponent<PlayerCombatManager>().targetEnemy = null;
    }

    public void TakeDamage(int damage)
    {
        if(!dead)
        {
            if (enemyHealth > damage)
            {
                enemyAnimation.SetTrigger("SOFT DAMAGE");
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
        enemyAnimation.SetTrigger("DIE");
        dead = true;
        GetComponent<NavMeshAgent>().speed = 0;
    }

}
