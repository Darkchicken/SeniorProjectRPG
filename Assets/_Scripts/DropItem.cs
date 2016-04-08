using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{
    public string dropTableId;
    public string dropItemId;
	
    public void GetDropItemId()
    {
        string[] items = { dropTableId };
        PlayFabApiCalls.GetLoot(items, gameObject);
        Invoke("DropItemToGround", 0.5f);
    }

    void DropItemToGround()
    {
        if(dropItemId != "Item_Empty")
        {
            GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
            item.GetComponent<TextMesh>().text = PlayFabDataStore.catalogItems[dropItemId].displayName;
            item.GetComponent<DroppedItem>().itemId = dropItemId;
            Debug.Log("Item Dropped : " + dropItemId);
        }
        else
        {
            GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
            item.GetComponent<TextMesh>().text = "Gold";
            item.GetComponent<DroppedItem>().itemId = dropItemId;
            Debug.Log("Item Dropped : " + dropItemId);
        }   
    }
}
