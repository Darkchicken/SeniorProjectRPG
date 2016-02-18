using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendsList : MonoBehaviour
{
    public GameObject friendPrefab;

    void Start()
    {
        LoadFriendsList();
    }

    void LoadFriendsList()
    {
        foreach(var friend in PlayFabDataStore.friendsList)
        {
            GameObject obj = Instantiate(friendPrefab);
            obj.transform.SetParent(this.transform, false);
            obj.GetComponentInChildren<Text>().text = friend.ToString();
        }
    }










}
