using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public Text enemyHealthText;
    public int enemyHealth;

    private Animator enemyAnimation;

    private bool dead;

    void Awake ()
    {
        enemyAnimation = GetComponent<Animator>();

	}

	void Update ()
    {

	}

    void OnMouseOver()
    {
        if(!dead)
        {
            enemyHealthText.text = "Enemy Health: " + enemyHealth;
        }
        
    }

    void OnMouseExit()
    {
        enemyHealthText.text = "";
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
        GetComponent<NavMeshAgent>().speed = 0;
    }

}
