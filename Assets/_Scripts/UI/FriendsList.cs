using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendsList : MonoBehaviour
{
    public GameObject friendPrefab;
    public GameObject addFriendPanel;
    public InputField friendEmail;

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
            obj.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
        }
    }

    public void AddFriend()
    {
        addFriendPanel.gameObject.SetActive(true);
    }

    public void AddFriendPlayFab()
    {
        PlayFabApiCalls.AddFriend(friendEmail.text);
    }

    public void CancelAddFriend()
    {
        addFriendPanel.gameObject.SetActive(false);
    }

    public void RemoveFriend()
    {

    }










}
