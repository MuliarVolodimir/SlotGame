using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotSystem : MonoBehaviour
{
    [SerializeField] int _slotCount;
    [SerializeField] int _spinPrice;
    [SerializeField] int _reward;

    [SerializeField] List<Item> _slotItems;
    [SerializeField] List<Image> _slotImages;
    [SerializeField] TextMeshProUGUI _coinsText;
    [SerializeField] TextMeshProUGUI _rewardText;

    [SerializeField] PopupScreen _popupScreen;
    [SerializeField] AudioClip _notMoneyClip;
    [SerializeField] AudioClip _spinClip;
    [SerializeField] AudioClip _winClip;
    [SerializeField] AudioClip _loseClip;

    private float _spinDuration;
    private List<int> _currentSlotValues;
    private bool _isSpinning = false;

    private void Start()
    {
        InitializeSlots();
        _rewardText.gameObject.SetActive(false);
    }

    private void InitializeSlots()
    {
        _currentSlotValues = new List<int>();

        for (int i = 0; i < _slotCount; i++)
        {
            int randomIndex = Random.Range(0, _slotItems.Count);
            _currentSlotValues.Add(randomIndex);
            _slotImages[i].sprite = _slotItems[randomIndex].Sprite;
        }
    }

    public void SpinSlots()
    {
        if (_isSpinning) return;
        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        _rewardText.gameObject.SetActive(false);

        var coins = ApplicationData.Instance.GameResource[0].Count;

        _spinDuration = _spinClip.length;
        if (coins >= _spinPrice)
        {
            coins -= _spinPrice;
            ApplicationData.Instance.GameResource[0].Count = coins;
            _coinsText.text = coins.ToString();

            _isSpinning = true;
            float elapsedTime = 0f;
            float interval = 0.1f;

            AudioManager.Instance.PlayOneShotSound(_spinClip);

            while (elapsedTime < _spinDuration)
            {
                for (int i = 0; i < _slotCount; i++)
                {
                    int randomIndex = Random.Range(0, _slotItems.Count);
                    _currentSlotValues[i] = randomIndex;
                    _slotImages[i].sprite = _slotItems[randomIndex].Sprite;
                }

                elapsedTime += interval;
                yield return new WaitForSeconds(interval);
            }

            
            StartCoroutine(CheckWinCondition());
        }
        else
        {
            AudioManager.Instance.PlayOneShotSound(_notMoneyClip);
            _popupScreen.ShowMessage("NOT ENOUGH COINS!");
        }
    }

    private IEnumerator CheckWinCondition()
    {
        bool isWin = true;

        for (int i = 1; i < _slotCount; i++)
        {
            if (_currentSlotValues[i] != _currentSlotValues[0])
            {
                isWin = false;
                break;
            }
        }
        
        _rewardText.gameObject.SetActive(true);

        if (isWin)
        {
            AudioManager.Instance.PlayOneShotSound(_winClip);
            
            _rewardText.text = $"WIN!!!";
            yield return new WaitForSeconds(_winClip.length);

            _popupScreen.ShowMessage($"WIN!\n +{_reward}");
            ApplicationData.Instance.GameResource[0].Count += _reward;
            var coins = ApplicationData.Instance.GameResource[0].Count;
            _coinsText.text = coins.ToString();
        }
        else
        {
            AudioManager.Instance.PlayOneShotSound(_loseClip);
            _rewardText.text = $"TRY AGAIN";
            yield return new WaitForSeconds(1f);

            _popupScreen.ShowMessage("TRY AGAIN!");
        }
        _isSpinning = false;
    }
}
