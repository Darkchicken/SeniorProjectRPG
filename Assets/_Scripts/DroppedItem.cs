using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour
{

    public string itemId;

    void Awake ()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void OnMouseDown()
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
        if(itemId == "Item_Gold")
        {
            Debug.Log("currency added");
            PlayFabApiCalls.AddUserCurrency(Random.Range(5, 50));
        }
        Debug.Log(itemId + " Item Looted from the ground");
        Destroy(gameObject);
        
    }

}
