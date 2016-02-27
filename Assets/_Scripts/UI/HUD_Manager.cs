using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class HUD_Manager : MonoBehaviour {

    public static HUD_Manager hudManager;

    public Canvas characterWindow;
    public Canvas runeWindow;
    public Canvas cheatWindow;
    public Canvas friendsWindow;

    public Image healthGlobe;
    public Image resourceGlobe;
    public Image enemyHealth;
    public Text enemyHealthText;
    public Text playerHealthText;

    public Text playerName;
    void Update()
    {
        healthGlobe.fillAmount = (float)PlayFabDataStore.playerCurrentHealth / (float)PlayFabDataStore.playerMaxHealth;
        resourceGlobe.fillAmount = (float)PlayFabDataStore.playerCurrentResource / (float)PlayFabDataStore.playerMaxResource;
        playerHealthText.text = PlayFabDataStore.playerCurrentHealth + "/" + PlayFabDataStore.playerMaxHealth;
    }

    void OnEnable()
    {
        SetPlayerNameOnUnitFrame();
        hudManager = this;
    }
	
    public void SetPlayerNameOnUnitFrame()
    {
        playerName.text = PlayFabDataStore.characterName;
    }
	public void ToggleCharacterWindow()
    {
        characterWindow.gameObject.SetActive(!characterWindow.gameObject.activeInHierarchy);
    }

    public void ToggleRuneWindow()
    {
        runeWindow.gameObject.SetActive(!runeWindow.gameObject.activeInHierarchy);
    }

    public void ToggleCheatPanel()
    {
        cheatWindow.gameObject.SetActive(!cheatWindow.gameObject.activeInHierarchy);
    }

    public void ToggleFriendsList()
    {
        friendsWindow.gameObject.SetActive(!friendsWindow.gameObject.activeInHierarchy);
    }
















































    /*void Update ()
    {
        if (Input.GetKeyDown("1"))
        {
            cheatPanel.gameObject.SetActive(!cheatPanel.gameObject.activeInHierarchy);
        }

        if (player == null)
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

        if (!runeUpdate)
        {
            runeUpdate = true;
            var request = new GetCharacterInventoryRequest()
            {
                CharacterId = PlayFabDataStore.characterId,
                //CatalogVersion = "Runes"
            };
            PlayFabClientAPI.GetCharacterInventory(request, (result) =>
            {
                foreach (var item in result.Inventory)
                {

                    if (item.ItemClass == "Skill")
                    {
                        PlayFabDataStore.playerSkillRunes.Add(item.ItemId, Runes.runes[item.ItemId]);
                    }
                    if (item.ItemClass == "Modifier")
                    {
                        PlayFabDataStore.playerModifierRunes.Add(item.ItemId, Runes.runes[item.ItemId]);
                    }
                }
            }, (error) =>
            {
                Debug.Log("Runes cannot retrieved!");
                Debug.Log(error.ErrorMessage);
                Debug.Log(error.ErrorDetails);
            });
        }
        
    }

    public void SelectSkillRunes(string runeName)
    {
        //Not working for more than 1 skill, will fix when rune UI is ready
        runeSelect = !runeSelect;
        Debug.Log(runeSelect);
        if(runeSelect)
        {
            Debug.Log("SELECTED");
            PlayFabDataStore.playerActiveSkillRunes.Add(PlayFabDataStore.playerSkillRunes[runeName], runeName);
        }
        else
        {
            Debug.Log("DESELECTED");
            PlayFabDataStore.playerActiveSkillRunes.Remove(PlayFabDataStore.playerSkillRunes[runeName]);
        }
        
    }*/

}
