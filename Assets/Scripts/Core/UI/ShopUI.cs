using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Image _slotImage;

    [SerializeField] Button _buyButton;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    [SerializeField] PopupScreen _popupScreen;
    [SerializeField] List<ShopItem> _shopItems;

    private int _itemsCount;
    private ShopItem _curResource;

    private void Start()
    {
        _itemsCount = _shopItems.Count;
        _buyButton.onClick.AddListener(() => { Buy(); });
        _leftButton.onClick.AddListener(() => { MoveLeft(); });
        _rightButton.onClick.AddListener(() => { MoveRight(); });
    }

    private void Buy()
    {
        var coins = ApplicationData.Instance.GameResource[0].Count;
        coins -= _curResource.Price;
        ApplicationData.Instance.GameResource[0].Count = coins;
    }
    
    private void MoveLeft()
    {
        _itemsCount--;
        if (_itemsCount <= _shopItems.Count) _itemsCount = _shopItems.Count - 1;
        UpdateGraphics();
    }

    private void MoveRight()
    {
        _itemsCount++;
        if (_itemsCount >= _shopItems.Count) _itemsCount = 0; 
        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        _slotImage.sprite = _shopItems[_itemsCount].Sprite;
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = _shopItems[_itemsCount].Price.ToString();
    }
}
