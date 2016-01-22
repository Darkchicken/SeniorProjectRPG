using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedPlayerScript : NetworkBehaviour
{
    public PlayerMovement playerMoveScript;

    public override void OnStartLocalPlayer()
    {
        playerMoveScript.enabled = true;
        gameObject.tag = "Player";

        gameObject.name = "LOCAL player";
        base.OnStartLocalPlayer();
    }
}
