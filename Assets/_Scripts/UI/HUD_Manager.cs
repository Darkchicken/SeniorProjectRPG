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
    public Canvas questWindow;
    public Canvas optionWindow;

    public Image healthGlobe;
    public Image resourceGlobe;
    public Image enemyHealth;
    public Text enemyHealthText;
    public Text playerHealthText;
    public Text playerResourceText;

    public Image ActionBarCooldownImage1;
    public Image ActionBarCooldownImage2;
    public Image ActionBarCooldownImage3;
    public Image ActionBarCooldownImage4;

    public Image ActionBarDisabledImage1;
    public Image ActionBarDisabledImage2;
    public Image ActionBarDisabledImage3;
    public Image ActionBarDisabledImage4;
    public Image ActionBarDisabledImage6;

    public Text playerName;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        healthGlobe.fillAmount = (float)PlayFabDataStore.playerCurrentHealth / (float)PlayFabDataStore.playerMaxHealth;
        resourceGlobe.fillAmount = (float)PlayFabDataStore.playerCurrentResource / (float)PlayFabDataStore.playerMaxResource;
        playerHealthText.text = PlayFabDataStore.playerCurrentHealth + "/" + PlayFabDataStore.playerMaxHealth;
        playerResourceText.text = PlayFabDataStore.playerCurrentResource + "/" + PlayFabDataStore.playerMaxResource;

        if(PlayFabDataStore.playerActiveSkillRunes.ContainsKey(1))
        {
            if (PlayFabDataStore.playerCurrentResource < PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[1]].resourceUsage)
            {
                ActionBarDisabledImage1.enabled = true;
            }
            else
            {
                ActionBarDisabledImage1.enabled = false;
            }
        }
        if (PlayFabDataStore.playerActiveSkillRunes.ContainsKey(2))
        {
            if (PlayFabDataStore.playerCurrentResource < PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[2]].resourceUsage)
            {
                ActionBarDisabledImage2.enabled = true;
            }
            else
            {
                ActionBarDisabledImage2.enabled = false;
            }
        }
        if (PlayFabDataStore.playerActiveSkillRunes.ContainsKey(3))
        {
            if (PlayFabDataStore.playerCurrentResource < PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[3]].resourceUsage)
            {
                ActionBarDisabledImage3.enabled = true;
            }
            else
            {
                ActionBarDisabledImage3.enabled = false;
            }
        }
        if (PlayFabDataStore.playerActiveSkillRunes.ContainsKey(4))
        {
            if (PlayFabDataStore.playerCurrentResource < PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[4]].resourceUsage)
            {
                ActionBarDisabledImage4.enabled = true;
            }
            else
            {
                ActionBarDisabledImage4.enabled = false;
            }
        }
        if (PlayFabDataStore.playerActiveSkillRunes.ContainsKey(6))
        {
            if (PlayFabDataStore.playerCurrentResource < PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[6]].resourceUsage)
            {
                ActionBarDisabledImage6.enabled = true;
            }
            else
            {
                ActionBarDisabledImage6.enabled = false;
            }
        }
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
        GetComponent<RaycastUI>().OnMouseExit();
        characterWindow.gameObject.SetActive(!characterWindow.gameObject.activeInHierarchy);
    }

    public void ToggleRuneWindow()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        runeWindow.gameObject.SetActive(!runeWindow.gameObject.activeInHierarchy);
        
    }

    public void ToggleCheatPanel()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        cheatWindow.gameObject.SetActive(!cheatWindow.gameObject.activeInHierarchy);
    }

    public void ToggleFriendsList()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        friendsWindow.gameObject.SetActive(!friendsWindow.gameObject.activeInHierarchy);
    }

    public void ToggleQuestWindow()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        questWindow.gameObject.SetActive(!questWindow.gameObject.activeInHierarchy);
    }

    public void ToggleOptionWindow()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        optionWindow.gameObject.SetActive(!optionWindow.gameObject.activeInHierarchy);
    }

    public void Logout()
    {
        PhotonNetwork.LoadLevel("Login");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToGame()
    {
        GetComponent<RaycastUI>().OnMouseExit();
        optionWindow.gameObject.SetActive(false);
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
