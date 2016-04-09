using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{
    public string dropTableId;
    public string dropItemId;
    public bool isItemReceived = false;
    private bool itemReceived = false;
	
    public void GetDropItemId()
    {
        string[] items = { dropTableId };
        PlayFabApiCalls.GetLoot(items, gameObject);

        StartCoroutine(DropItemToGround());
    }

    //Checks until item information returns from the playFab
    IEnumerator DropItemToGround()
    {
        Debug.Log("Dropitemid 1" + dropItemId);
        if(isItemReceived)
        {
            if (dropItemId != "Item_Empty")
            {
                Debug.Log("Dropitemid 2" + dropItemId);
                GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
                item.GetComponent<TextMesh>().text = PlayFabDataStore.catalogItems[dropItemId].displayName;
                item.GetComponent<DroppedItem>().itemId = dropItemId;
                Debug.Log("Item Dropped : " + dropItemId);
            }
            else
            {
                Debug.Log("Dropitemid 3" + dropItemId);
                GameObject item = PhotonNetwork.Instantiate("DropItem", transform.position, transform.rotation, 0);
                item.GetComponent<TextMesh>().text = "Gold";
                item.GetComponent<DroppedItem>().itemId = dropItemId;
                Debug.Log("Item Dropped : " + dropItemId);
            }
            itemReceived = true; // this is to double check the condition, in case of isItemreceived becomes true after the if statement
        }
        Debug.Log("Dropitemid 4" + dropItemId);


        yield return new WaitForSeconds(0.5f);

        if(isItemReceived == false || itemReceived == false)
        {
            StartCoroutine(DropItemToGround());
        }
        
    }
}
