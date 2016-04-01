﻿using UnityEngine;
using System.Collections;

public class AreaTeleport : MonoBehaviour {

    public string levelToTeleport;
    GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            PhotonNetwork.LoadLevel(levelToTeleport);
        }
    }

   
}
