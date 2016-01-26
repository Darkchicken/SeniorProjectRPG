using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        

	}
    public void Respawn()
    {
        //if player is dead
        if (playerHealth.IsPlayerDead())
        {
            //set player poisition to position of the spawnpoint
            player.transform.position = transform.position;
            player.tag = "Player";
            //player.GetComponent<NavMeshAgent>().speed = 0;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
