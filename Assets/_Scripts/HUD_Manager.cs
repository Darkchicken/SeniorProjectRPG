using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HUD_Manager : MonoBehaviour {

    public Slider playerHealthSlider;
    public Slider playerResourceSlider;
    public Slider enemyHealthSlider;
    public Text playerHealthText;
    public Text playerResourceText;
    public Canvas runePanel;


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
            playerHealth = player.GetComponent<Health>().GetHealth();
            playerHealthSlider.value = playerHealth;
            playerHealthText.text = playerHealth + "/100";
            if(playerHealth == 0)
            {
                playerHealthSlider.gameObject.SetActive(false);
            }
            playerResourceSlider.value = player.GetComponent<PlayerCombatManager>().GetPlayerResource();
            playerResourceText.text = playerResourceSlider.value + "/100";

        }
    }

    public void ToggleRunePanel()
    {
        runePanel.gameObject.SetActive(!runePanel.gameObject.activeInHierarchy);
    }

    
}
