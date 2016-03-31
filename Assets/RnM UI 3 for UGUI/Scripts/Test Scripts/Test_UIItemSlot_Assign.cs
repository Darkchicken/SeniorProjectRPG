using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test_UIItemSlot_Assign : MonoBehaviour {
	
	public UIItemSlot slot;
	public UIItemDatabase itemDatabase;
	public int assignItem;
	
	void OnEnable()
	{
		if (this.slot == null)
			this.slot = this.GetComponent<UIItemSlot>();
        if (PlayFabDataStore.playerInventory.Count >= assignItem)
        {
            itemDatabase.items.Add(PlayFabDataStore.catalogItems[PlayFabDataStore.playerInventory[assignItem - 1].itemId]);
        }
            
	}
	
	void Start()
	{
        if (this.slot == null || PlayFabDataStore.playerInventory == null)
        {
            this.Destruct();
            return;
        }
        slot.Assign(itemDatabase.GetByID(assignItem - 1));
        Destruct();

    }

    private void Destruct()
	{
		DestroyImmediate(this);
	}

    void OnApplicationQuit()
    {
        itemDatabase.items.Clear();
    }
}
