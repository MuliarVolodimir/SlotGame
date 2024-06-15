using System;
using TMPro;
using UnityEngine;
//using UnityEngine.Purchasing;

public class PurchaseSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coins;
    [SerializeField] PopupScreen _popupScreen;

    // public void OnPurchaseComplite(Product product)
    // {
    //     switch (product.definition.id)
    //     {
    //         case "spaceSlot10000Coins":
    //             PurchaseCoins();
    //             break;
    //         default:
    //             break;
    //     }
    // }

    // public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    // {
    //     switch (product.definition.id)
    //     {
    //         case "spaceSlot10000Coins":
    //             _popupScreen.ShowMessage($"FAILD TO PURCHASE COINS! \n {failureReason}");
    //             break;
    //         default:
    //             break;
    //     }
        
    // }

    private void PurchaseCoins()
    {
        ApplicationData.Instance.GameResource[0].Count += 10000;
        _coins.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _popupScreen.ShowMessage("SUCSSESFUL PURCHASE!");
    }
}
