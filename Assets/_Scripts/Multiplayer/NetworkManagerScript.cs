using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    GameObject player;
    PlayerCombatManager combatManager;
    Runes playerRunes;
    CameraFollow cameraFollow;
    Health playerHealth;
    public Transform spawnPoint;
    public Transform enemySpawnPoint;
    // Use this for initialization
    void Awake ()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate("Orc", enemySpawnPoint.position, enemySpawnPoint.rotation, 0);
        }
        player = PhotonNetwork.Instantiate("Elf", spawnPoint.position, spawnPoint.rotation, 0);
        combatManager = player.GetComponent<PlayerCombatManager>();
        combatManager.enabled = true;
        playerRunes = player.GetComponent<Runes>();
        playerRunes.enabled = true;
        //player.GetComponent<Health>().enabled = true;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.enabled = true;
        playerHealth = player.GetComponent<Health>();
        //playerHealth.enabled = true;

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnJoinedRoom()
    {
        PlayFabDataStore.playerCurrentHealth = PlayFabDataStore.playerMaxHealth;
        PlayFabDataStore.playerCurrentResource = 0;
    }
    void OnPhotonPlayerConnected(PhotonPlayer connected)
    {
        Debug.Log("New Player Joined Room!");
        //this doesnt work currently
        //Debug.Log(connected.name);
    }
}
