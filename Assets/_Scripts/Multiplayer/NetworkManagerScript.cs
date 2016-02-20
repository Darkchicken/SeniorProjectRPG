using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    PlayerCombatManager combatManager;
    Runes playerRunes;
    CameraFollow cameraFollow;
    public Transform spawnPoint;
	// Use this for initialization
	void Awake ()
    {
     
        GameObject player = PhotonNetwork.Instantiate("PlayerCharacter", spawnPoint.position, spawnPoint.rotation, 0);
        combatManager = player.GetComponent<PlayerCombatManager>();
        combatManager.enabled = true;
        playerRunes = player.GetComponent<Runes>();
        playerRunes.enabled = true;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnJoinedRoom()
    {
        
    }
}
