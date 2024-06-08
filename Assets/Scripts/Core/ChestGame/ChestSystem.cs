using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] int _tapsToOpen = 5;
    [SerializeField] float _chestOpenDuration;
    [SerializeField] Button _tapButton;

    [SerializeField] Image _chestSlot;
    [SerializeField] Sprite _chestSprite;
    [SerializeField] List<Item> _possibleItems;

    [SerializeField] GameObject _breakParticle;
    [SerializeField] Transform _particleSpawnPos;
    [SerializeField] GameObject _popupScreen;

    [SerializeField] TextMeshProUGUI _findText;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] Image _itemImage;

    [SerializeField] TextMeshProUGUI _tapCounterText;
    [SerializeField] TextMeshProUGUI _coinsText;

    [SerializeField] List<AudioClip> _tapClips;
    [SerializeField] AudioClip _openChestClip;

    [SerializeField] Animator _chestAnimator;
    
    private int _currentTaps;

    private void Start()
    {
        _coinsText.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _tapButton.onClick.AddListener(() => { TapChest(); });
        FindObjectOfType<TutorialSystem>().OnConfirm += ChestSystem_OnConfirm;
        
    }

    private void ChestSystem_OnConfirm()
    {
        ApplicationData.Instance.ChestFirstStart = false;
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

            _chestAnimator.ResetTrigger("Hit");
            _chestAnimator.SetTrigger("Hit");

            _currentTaps++;
            UpdateTapCounterText();

            if (_currentTaps >= _tapsToOpen)
            {
                Color chestColor = _chestSlot.color;
                chestColor.a = 0f;

                _chestSlot.color = chestColor;
                StartCoroutine(OpenChest());
            }
        }
    }

    private IEnumerator OpenChest()
    {
        AudioManager.Instance.PlayOneShotSound(_openChestClip);

        GameObject particle = Instantiate(_breakParticle, _particleSpawnPos);
        Destroy(particle, 1f);
        yield return new WaitForSeconds(_chestOpenDuration);

        var isFind = false;
        isFind = Random.Range(0, 2) == 1 ? true : false;
        
        _popupScreen.SetActive(true);
        if (isFind)
        {
            var item = _possibleItems[Random.Range(0, _possibleItems.Count)];
            ApplicationData.Instance.GameResource[0].Count += item.Price;

            _findText.text = $"FIND ITEM";
            _priceText.text = $"+{item.Price}";

            Color color = _itemImage.color;
            color.a = 1f;
            _itemImage.color = color;
            _itemImage.sprite = item.Sprite;
            _popupScreen.GetComponent<PopupScreen>().OnConfirm += SpawnChest;
        }
        else
        {
            _findText.text = $"EMPTY";
            _priceText.text = $" ";
            Color color = _itemImage.color;
            color.a = 0f;
            _itemImage.color = color;
            _popupScreen.GetComponent<PopupScreen>().OnConfirm += SpawnChest;
        }
    }

    private void SpawnChest()
    {
        _coinsText.text = ApplicationData.Instance.GameResource[0].Count.ToString();
        _currentTaps = 0;

        Color chestColor = _chestSlot.color;
        chestColor.a = 1f;

        _chestSlot.color = chestColor;
        _chestSlot.sprite = _chestSprite;
        _popupScreen.GetComponent<PopupScreen>().OnConfirm -= SpawnChest;  
        
    }
}