using System;
using System.Linq;
using SlotGameVova.Assets.Scripts.Purchasing;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class PurchaseSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coins;
    [SerializeField] PopupScreen _popupScreen;

    [Space]
    [SerializeField] private string[] _productsIDs;
    [SerializeField] private int[] _valuesRewardCoins;
    [SerializeField] private TextMeshProUGUI _textReward;
    [Space]
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _restorePurchases;


    private int _nubmerProductNow;


    private void Awake()
    {
        _nubmerProductNow = 0;
    }

    private void OnEnable()
    {
        _nubmerProductNow = 0;
        InitializeButtonListeners();
        UpdateProductDisplay();
    }

    private void InitializeButtonListeners()
    {
        _prevButton.onClick.AddListener(PrevProduct);
        _nextButton.onClick.AddListener(NextProduct);
        _restorePurchases.onClick.AddListener(RestorePurchases);
    }

    private void RestorePurchases()
    {
        IAPPurchasingInitData.Instance.OnRestorePurchases();
    }

    private void UpdateProductDisplay()
    {

        _textReward.text = $"+{_valuesRewardCoins[_nubmerProductNow]} Coins";

    }

    private void PrevProduct()
    {
        _nubmerProductNow = (_nubmerProductNow - 1 + _productsIDs.Length) % _productsIDs.Length;
        UpdateProductDisplay();
    }

    private void NextProduct()
    {
        _nubmerProductNow = (_nubmerProductNow + 1) % _productsIDs.Length;
        UpdateProductDisplay();
    }

    public void OnPurchaseButtonEvent()
    {
        Debug.Log("Work");
        Action actionPurchased = () => { PurchaseCoins(_nubmerProductNow); };

        IAPPurchasingInitData.Instance.ByIdPurchase(_productsIDs[_nubmerProductNow], actionPurchased);
    }
    public void OnPurchaseComplite(UnityEngine.Purchasing.Product product)
    {
        if (_productsIDs.Contains(product.definition.id))
        {
            Action actionPurchased = () => { PurchaseCoins(_nubmerProductNow); };

            IAPPurchasingInitData.Instance.ByIdPurchase(product.definition.id, actionPurchased);
        }
        else
        {
            Debug.LogWarning($"Product ID {product.definition.id} not found in _productsIDs.");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
    {
        switch (product.definition.id)
        {
            case "spaceSlot10000Coins":
                _popupScreen.ShowMessage($"FAILD TO PURCHASE COINS! \n {failureReason}");
                break;
            default:
                break;
        }
    }



    private void PurchaseCoins(int _nubmerProductNow)
    {
        ApplicationData.Instance.GameResource[0].Count += _valuesRewardCoins[_nubmerProductNow];
        _coins.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _popupScreen.ShowMessage("SUCSSESFUL PURCHASE!");
    }

    private void OnDisable()
    {
        _prevButton.onClick.RemoveAllListeners();
        _nextButton.onClick.RemoveAllListeners();
        _restorePurchases.onClick.RemoveAllListeners();

    }
}
