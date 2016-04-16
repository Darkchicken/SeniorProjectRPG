﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkEnemyScript : MonoBehaviour
{
    private PhotonView photonView;
    private Animator anim;

    private Vector3 enemyPosition = Vector3.zero;
    private Quaternion enemyRotation = Quaternion.identity;
    private int playerHealth;


    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        playerHealth = GetComponent<Health>().health;
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (photonView.isMine)
        {
            //do nothing, character is being controlled by player
        }
        else
        {
            //prevent syncing on entrance to room
            if (enemyPosition != Vector3.zero)
            {
                transform.position = Vector3.Lerp(transform.position, enemyPosition, 0.1f);
                transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, 0.1f);
            }
        }

    }

    [PunRPC]
    void SendTrigger(int sentId, string triggerName)
    {
        //Debug.Log("I received an animation trigger for " + sentId + " and my id is " + photonView.viewID);
        if (photonView.viewID == sentId)
        {
            anim.SetTrigger(triggerName);
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
                //stream.SendNext(anim.GetBool("INCOMBAT"));
                //stream.SendNext(anim.GetBool("Attack"));
            }
        }
        else
        {
            //Network player, receive data
            enemyPosition = (Vector3)stream.ReceiveNext();
            enemyRotation = (Quaternion)stream.ReceiveNext();
            //receive animator variables from other player
            if (anim != null)
            {
                anim.SetFloat("MOVE", (float)stream.ReceiveNext());
                //anim.SetBool("INCOMBAT", (bool)stream.ReceiveNext());
                //anim.SetBool("Attack", (bool)stream.ReceiveNext());
            }

        }
    }
}
