using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HUD_Manager : MonoBehaviour {

    public Slider playerHealthSlider;
    public Text playerHealthText;

    private GameObject player;
    private int playerHealth;
	

	void Update ()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            playerHealth = player.GetComponent<PlayerHealth>().GetPlayerHealth();
            playerHealthSlider.value = playerHealth;
            playerHealthText.text = playerHealth + "/100";
            if(playerHealth == 0)
            {
                playerHealthSlider.gameObject.SetActive(false);
            }

        }

        


    }
}
