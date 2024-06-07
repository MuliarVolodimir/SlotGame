using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Image _slotImage;
    [SerializeField] TextMeshProUGUI _coinsText;

    [SerializeField] Button _buyButton;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    [SerializeField] PopupScreen _popupScreen;
    [SerializeField] List<Item> _shopItems;

    [SerializeField] AudioClip _aplayClip;
    [SerializeField] AudioClip _notMoneyClip;

    private int _itemIndex;
    private Item _curResource;
    

    private void Start()
    {
        _itemIndex = _shopItems.Count - 1;

        _buyButton.onClick.AddListener(() => { Buy(); });
        _leftButton.onClick.AddListener(() => { MoveLeft(); });
        _rightButton.onClick.AddListener(() => { MoveRight(); });

        UpdateGraphics();
    }

    private void Buy()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);

        var coins = ApplicationData.Instance.GameResource[0].Count;
        if (coins >= _curResource.Price)
        {
            coins -= _curResource.Price;
            ApplicationData.Instance.GameResource[0].Count = coins;
            _coinsText.text = coins.ToString();
        }
        else
        {
            AudioManager.Instance.PlayOneShotSound(_notMoneyClip);
            _popupScreen.ShowMessage("NOT ENOUGHT COINS!");
        }
    }
    
    private void MoveLeft()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _itemIndex--;
        if (_itemIndex < 0) _itemIndex = _shopItems.Count - 1;
        UpdateGraphics();
    }

    private void MoveRight()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _itemIndex++;
        if (_itemIndex >= _shopItems.Count) _itemIndex = 0; 
        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        _curResource = _shopItems[_itemIndex];

        _slotImage.sprite = _shopItems[_itemIndex].Sprite;
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = _shopItems[_itemIndex].Price.ToString();
    }
}
