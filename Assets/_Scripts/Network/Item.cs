using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class Item : MonoBehaviour {

    public StoreItem StoreItem;
    public Text ItemDisplay;
    public Text ItemDesc;
    public Image ItemImage;
    public Button BuyButton;

    void OnEnable()
    {
        BuyButton.onClick.AddListener(BuyItem);
    }

    void OnDisable()
    {
        BuyButton.onClick.RemoveListener(BuyItem);
    }

    private void BuyItem()
    {
        List<ItemPuchaseRequest> itemsToPurchase = new List<ItemPuchaseRequest>();
        itemsToPurchase.Add(new ItemPuchaseRequest()
        {
            ItemId = StoreItem.ItemId,
            Quantity = 1
        });

        var startRequest = new StartPurchaseRequest()
        {
            StoreId = "Character",
            Items = itemsToPurchase,

        };
        PlayFabClientAPI.StartPurchase(startRequest, (startResult) =>
        {
            //Store the order and move to confirmation screen.
            PlayFabDataStore.Orders.Enqueue(startResult);
            //WindowManager.SendEvent("Purchase");
        }, PlayFabErrorHandler.HandlePlayFabError);
    }
}
