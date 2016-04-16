using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour
{

    public string itemId;
    private PhotonView photonView;

    void Awake ()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
        photonView = GetComponent<PhotonView>();
    }

    public void OnMouseDown()
    {
        photonView.RPC("LootTheItem", PhotonTargets.AllViaServer, PhotonNetwork.player.ID);
    }

    [PunRPC]
    void LootTheItem(int playerID)
    {
        if(PhotonNetwork.player.ID == playerID)
        {
            if (PlayFabDataStore.playerInventory.Count < PlayFabDataStore.playerInventorySlotCount)
            {
                string[] items = { itemId };
                if (PlayFabDataStore.catalogItems.ContainsKey(itemId))
                {
                    PlayFabApiCalls.GrantItemsToCharacter(items, "IsEquipped", "Item");

                }
                else if (PlayFabDataStore.catalogRunes.ContainsKey(itemId))
                {
                    PlayFabApiCalls.GrantItemsToCharacter(items, "Active", "Rune");
                }
            }
            if (itemId == "Item_Gold")
            {
                Debug.Log("currency added");
                PlayFabApiCalls.AddUserCurrency(Random.Range(5, 50));
            }
            Debug.Log(itemId + " Item Looted from the ground");
        }
        if(photonView.isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        
    }

}
