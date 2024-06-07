using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] int _tapsToOpen = 5;
    [SerializeField] Button _tapButton;

    [SerializeField] Image _chestSlot;
    [SerializeField] Sprite _chestSprite;
    [SerializeField] List<Item> _possibleItems;

    [SerializeField] PopupScreen _popupScreen;

    [SerializeField] TextMeshProUGUI _tapCounterText;
    [SerializeField] TextMeshProUGUI _coinsText;

    [SerializeField] List<AudioClip> _tapClips;
    [SerializeField] AudioClip _openChestClip;
    
    private int _currentTaps;

    private void Start()
    {
        _coinsText.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _tapButton.onClick.AddListener(() => { TapChest(); });
        UpdateTapCounterText();
        SpawnChest();
    }

    private void UpdateTapCounterText()
    {
        _tapCounterText.text = $"Taps left: {_tapsToOpen - _currentTaps}";
    }

    private void TapChest()
    {
        if (_currentTaps < _tapsToOpen)
        {
            var index = Random.Range(0, _tapClips.Count);
            AudioManager.Instance.PlayOneShotSound(_tapClips[index]);
            _currentTaps++;
            UpdateTapCounterText();

            if (_currentTaps >= _tapsToOpen)
            {
                _chestSlot.sprite = null;
                OpenChest();
            }
        }
    }

    private void OpenChest()
    {
        AudioManager.Instance.PlayOneShotSound(_openChestClip);

        var isFind = false;
        isFind = Random.Range(0, 2) == 1 ? true : false;

        if (isFind)
        {
            var item = _possibleItems[Random.Range(0, _possibleItems.Count)];
            ApplicationData.Instance.GameResource[0].Count += item.Price;

            
            _popupScreen.ShowMessage($"FIND ITEM: {item.Name} \n +{item.Price}");
            _popupScreen.OnConfirm += SpawnChest;
        }
        else
        {
            _popupScreen.ShowMessage($"EMPTY");
            _popupScreen.OnConfirm += SpawnChest;
        }
    }

    private void SpawnChest()
    {
        _coinsText.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _currentTaps = 0;
        _chestSlot.sprite = _chestSprite;
        _popupScreen.OnConfirm -= SpawnChest;  
        
    }
}