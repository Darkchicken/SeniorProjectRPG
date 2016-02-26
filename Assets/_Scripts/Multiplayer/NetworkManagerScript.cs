using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    PlayerCombatManager combatManager;
    Runes playerRunes;
    CameraFollow cameraFollow;
    public Transform spawnPoint;
    public Transform enemySpawnPoint;
    // Use this for initialization
    void Awake ()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate("Enemy", enemySpawnPoint.position, enemySpawnPoint.rotation, 0);
        }
        GameObject player = PhotonNetwork.Instantiate("PlayerCharacter", spawnPoint.position, spawnPoint.rotation, 0);
        combatManager = player.GetComponent<PlayerCombatManager>();
        combatManager.enabled = true;
        playerRunes = player.GetComponent<Runes>();
        playerRunes.enabled = true;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.enabled = true;
        player.GetComponent<Health>().enabled = true;

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnJoinedRoom()
    {
        
    }
    void OnPhotonPlayerConnected(PhotonPlayer connected)
    {
        Debug.Log("New Player Joined Room!");
        //this doesnt work currently
        //Debug.Log(connected.name);
    }
}
