using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{
    public string dropTableId;
    public string dropItemId;
    public bool isItemReceived = false;
    private bool itemReceived = false;
    private PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
	
    public void GetDropItemId()
    {
        string[] items = { dropTableId };
        PlayFabApiCalls.GetLoot(items, gameObject);

        StartCoroutine(DropItemToGround());
    }

    //Checks until item information returns from the playFab
    IEnumerator DropItemToGround()
    {
        Debug.Log("Dropitemid " + dropItemId);
        if(isItemReceived)
        {
            if (dropItemId != "Item_Gold")
            {
                GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
                photonView.RPC("SetItemDetails", PhotonTargets.AllBufferedViaServer, item.GetComponent<PhotonView>().viewID, PlayFabDataStore.catalogItems[dropItemId].displayName, dropItemId);
                /*item.GetComponent<TextMesh>().text = PlayFabDataStore.catalogItems[dropItemId].displayName;
                item.GetComponent<DroppedItem>().itemId = dropItemId;*/
                Debug.Log("Item Dropped : " + dropItemId);
            }
            else
            {
                GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
                photonView.RPC("SetItemDetails", PhotonTargets.AllBufferedViaServer, item.GetComponent<PhotonView>().viewID, "Gold", dropItemId);
                /*item.GetComponent<TextMesh>().text = "Gold";
                item.GetComponent<DroppedItem>().itemId = dropItemId;*/
                Debug.Log("Item Dropped : " + dropItemId);
            }
            itemReceived = true; // this is to double check the condition, in case of isItemreceived becomes true after the if statement
        }


        yield return new WaitForSeconds(0.5f);

        if(isItemReceived == false || itemReceived == false)
        {
            StartCoroutine(DropItemToGround());
        }
        
    }

    [PunRPC]
    void SetItemDetails(int sourceID, string name, string itemID)
    {
        GameObject source = PhotonView.Find(sourceID).gameObject;
        source.GetComponent<TextMesh>().text = name;
        source.GetComponent<DroppedItem>().itemId = itemID;
    }
}
