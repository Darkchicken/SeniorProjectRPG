using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

    void Start()
    {
        //version number of game is passed as variable
        Debug.Log("PHOTON connection initialized");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
        Debug.Log("Arrives here");
        
       
    }
    void Update()
    {
        if (!PhotonNetwork.connected)
        {
            Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else
        {
            Debug.Log("AttemptingToConnect");
            PhotonNetwork.CreateRoom(null);
        }
    }



    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
    }




}
