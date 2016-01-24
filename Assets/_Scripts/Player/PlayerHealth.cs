using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public Text playerHealthText;
    public int playerHealth = 100;

    private Animator playerAnimation;

    private bool dead = false;

    void Awake()
    {
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        playerHealthText.text = "Player Health: " + playerHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!dead)
        {
            if (playerHealth > damage)
            {
                playerAnimation.SetTrigger("SOFT DAMAGE");
                playerHealth -= damage;
            }
            else
            {
                PlayerDead();
            }
        }
    }

    void PlayerDead()
    {
        playerAnimation.SetTrigger("DIE");
        GetComponent<NavMeshAgent>().speed = 0;
    }
}
