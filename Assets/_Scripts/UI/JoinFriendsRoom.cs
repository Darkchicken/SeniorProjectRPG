using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoinFriendsRoom : MonoBehaviour
{

    public void JoinRoom(Text name)
    {
        PlayFabDataStore.friendUsername = name.text;
        PlayFabApiCalls.GetAllUsersCharacters(PlayFabDataStore.friendsList[PlayFabDataStore.friendUsername], "Friend");
    }

}
