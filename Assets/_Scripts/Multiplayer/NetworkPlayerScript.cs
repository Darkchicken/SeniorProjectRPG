using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerScript : NetworkBehaviour {


    Renderer[] renderers;
    // Use this for initialization
    void Start ()
    {
        renderers = GetComponentsInChildren<Renderer>();
        //set proper name and tag to distinguish local player from others
        if (isLocalPlayer)
        {
            gameObject.tag = "Player";
            gameObject.name = "LOCAL player";
        }
        else
        {
            //if this is the arena
            if (SceneManager.GetActiveScene().name == "BattleArena")
            {
                gameObject.tag = "Enemy";
                gameObject.name = "Enemy";
            }
            else
            {
                gameObject.tag = "NetworkAlly";
                gameObject.name = "Network Ally";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ToggleRenderer(bool isAlive)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].enabled = isAlive;
    }

    [ClientRpc]
    public void RpcResolveHit()
    {
        //currently destroys player, will change to take damage later
        ToggleRenderer(false);

        if (isLocalPlayer)
        {
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;

        }

        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        ToggleRenderer(true);
    }
}
