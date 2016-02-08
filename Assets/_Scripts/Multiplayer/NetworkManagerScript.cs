using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    PlayerCombatManager combatManager;
    CameraFollow cameraFollow;
    public Transform spawnPoint;
	// Use this for initialization
	void Start ()
    {
     
        GameObject player = PhotonNetwork.Instantiate("PlayerCharacter", spawnPoint.position, spawnPoint.rotation, 0);
        combatManager = player.GetComponent<PlayerCombatManager>();
        combatManager.enabled = true;
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
