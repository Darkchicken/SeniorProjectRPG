using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public Text playerHealthText;
    public Slider playerHealthSlider;
    public int playerHealth = 100;

    private Animator playerAnimation;

    private bool dead = false;

    void Awake()
    {
        playerAnimation = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {

        if (!dead)
        {
            if (playerHealth > damage)
            {
                playerAnimation.SetTrigger("TAKE DAMAGE 1");
                playerHealth -= damage;
            }
            else
            {
                playerHealth = 0;
                PlayerDead();
            }
        }
    }

    void PlayerDead()
    {
        dead = true;
        gameObject.tag = "Respawn";
        playerAnimation.SetTrigger("DIE");
        GetComponent<NavMeshAgent>().speed = 0;
        GetComponent<PlayerMovement>().enabled = false;    
    }

    public bool IsPlayerDead()
    {
        return dead;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
