using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkPlayerScript : MonoBehaviour {

    bool battleArena = true;
    PhotonView photonView;
    Animator anim;

    Vector3 playerPos = Vector3.zero;
    Quaternion playerRot = Quaternion.identity;
    int playerHealth;


    // Use this for initialization
    void Start ()
    {
        //get the photon view of this character
        photonView = gameObject.GetComponent<PhotonView>();
        //get player's health
        playerHealth = GetComponent<Health>().health;
        //get Players animator
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("This has no animator attached to sync");
        }
       
        //set proper name and tag to distinguish local player from others
        if (photonView.isMine)//isLocalPlayer)
        {
            gameObject.tag = "Player";
            gameObject.name = "LOCAL player";
            //new name change
            //gameObject.name = photonView.ownerId.ToString();
        }
        else
        {
          
            if (battleArena)//SceneManager.GetActiveScene().name == "BattleArena")
            {
                gameObject.tag = "Enemy";
                //set player's layer to default so you can click on them
                gameObject.layer = LayerMask.NameToLayer("Default");
                gameObject.name = "Network Enemy";
                //gameObject.name = photonView.ownerId.ToString();
            }
            else
            {
                gameObject.tag = "Player";
                gameObject.name = "Network player";
                //gameObject.name = photonView.ownerId.ToString();
            }
           
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            //do nothing, character is being controlled by player
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRot, 0.1f);
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //send all animator variables
            if (anim != null)
            {
                stream.SendNext(anim.GetFloat("MOVE"));
                stream.SendNext(anim.GetBool("INCOMBAT"));
                stream.SendNext(anim.GetBool("Attack"));
            }
        }
        else
        {
            //Network player, receive data
            playerPos = (Vector3)stream.ReceiveNext();
            playerRot = (Quaternion)stream.ReceiveNext();
            //receive animator variables from other player
            if (anim != null)
            {
                anim.SetFloat("MOVE", (float)stream.ReceiveNext());
                anim.SetBool("INCOMBAT", (bool)stream.ReceiveNext());
                anim.SetBool("Attack", (bool)stream.ReceiveNext());
            }

        }
    }


}
