using UnityEngine;
using System.Collections;

public class AreaTeleport : MonoBehaviour {

    public string levelToTeleport;
    public string spawnPointName = "SpawnPoint";

    GameObject player;
    NetworkManagerScript networkManager;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null && Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            if (spawnPointName != null)
            {
               networkManager.spawnPointName = spawnPointName;
            }
            PhotonNetwork.LoadLevel(levelToTeleport);
        }
    }

   
}
