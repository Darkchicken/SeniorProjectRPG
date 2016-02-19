using UnityEngine;
using System.Collections;
using Photon;

public class PhotonCalls : PunBehaviour
{

    //exits the current room
    public static void LeaveRoom()
    {
        
        PhotonNetwork.LeaveRoom();
    }
    //when the player leaves their current room, reenter the lobby
    public override void OnLeftRoom()
    {
        PhotonNetwork.JoinLobby();
    }
    //upon reaching the lobby, join a random room 
    public override void OnJoinedLobby()
    {
        Debug.Log("Looking for room to join");
        PhotonNetwork.JoinRandomRoom();
    }

    //if the player fails to join a random room
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        Debug.Log("Creating a new room!");
        ///argument is room name (null to assign a random name)
        PhotonNetwork.CreateRoom("Tester");

       
    }
    //upon joining a new room, output the room name
    public override void OnJoinedRoom()
    {

        Debug.Log("Join Room Successfully!");
        Debug.Log("Room name is: " + PhotonNetwork.room);

        //GameObject player = PhotonNetwork.Instantiate("PlayerCharacter", spawnPoint.position, Quaternion.identity, 0);
        //player.GetComponent<PlayerCombatManager>().enabled = true;

    }


}

