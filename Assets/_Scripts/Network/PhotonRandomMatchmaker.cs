using UnityEngine;
using System.Collections;
using Photon;

public class PhotonRandomMatchmaker : PunBehaviour
{
    public Transform spawnPoint;
    public Transform enemySpawnPoint;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }


    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
        
        Debug.Log("Creating a new room!");

    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("PlayerCharacter", spawnPoint.position, Quaternion.identity, 0);
        player.GetComponent<PlayerCombatManager>().enabled = true;

    }


}
