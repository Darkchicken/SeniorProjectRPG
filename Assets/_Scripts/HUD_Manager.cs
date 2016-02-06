using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


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
            playerResourceSlider.value = PlayFabDataStore.playerResource;
            playerResourceText.text = playerResourceSlider.value + "/100";

        }
    }

    public void ToggleRunePanel()
    {
        runePanel.gameObject.SetActive(!runePanel.gameObject.activeInHierarchy);

        if(runePanel.gameObject.activeInHierarchy)
        {
            var request = new GetCharacterInventoryRequest()
            {
                CharacterId = PlayFabDataStore.characterId,
                CatalogVersion = "Runes"
            };
            PlayFabClientAPI.GetCharacterInventory(request, (result) =>
            {
                //Debug.Log(result.Inventory.Count);
                foreach (var item in result.Inventory)
                {
                    //Debug.Log(item.DisplayName);
                    //PlayFabDataStore.playerSkillRunes.Add(item.DisplayName, 5);
                    //Debug.Log("Runes: " + Runes.runes[item.DisplayName]);

                    if (item.ItemClass == "Skill")
                    {
                        //Debug.Log("Skill: " + item.DisplayName);
                        PlayFabDataStore.playerSkillRunes.Add(item.DisplayName, Runes.runes[item.DisplayName]);
                    }
                    if (item.ItemClass == "Modifier")
                    {
                        //Debug.Log("Modifier: " + item.DisplayName);
                        PlayFabDataStore.playerModifierRunes.Add(item.DisplayName, Runes.runes[item.DisplayName]);
                    }
                }
            }, (error) =>
            {
                Debug.Log("Runes cannot retrieved!");
                //Debug.Log(error.ErrorMessage);
                //Debug.Log(error.ErrorDetails);
            });
        }
        
    }

    public void SelectSkillRunes(string runeName)
    {
        Debug.Log("SELECTED");
        //Debug.Log(PlayFabDataStore.playerSkillRunes["Slam"]);
        PlayFabDataStore.playerActiveSkillRunes.Add(PlayFabDataStore.playerSkillRunes[runeName], runeName);
        //Debug.Log(PlayFabDataStore.playerActiveSkillRunes[0]);
    }

    public void DeselectSkillRunes(string runeName)
    {
        Debug.Log("DESELECTED");
        PlayFabDataStore.playerActiveSkillRunes.Remove(PlayFabDataStore.playerSkillRunes[runeName]);

    }

}
