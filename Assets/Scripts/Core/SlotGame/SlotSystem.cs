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

    [SerializeField] PopupScreen _popupScreen;

    [SerializeField] float _spinDuration; 

    private List<int> _currentSlotValues;
    private bool _isSpinning = false;
    

    private void Start()
    {
        InitializeSlots();
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
        SpinCoroutine();
    }

    private void SpinCoroutine()
    {
        var coins = ApplicationData.Instance.GameResource[0].Count;

        if (coins >= _spinPrice)
        {
            coins -= _spinPrice;
            ApplicationData.Instance.GameResource[0].Count = coins;
            _coinsText.text = coins.ToString();

            _isSpinning = true;
            float elapsedTime = 0f;

            while (elapsedTime <= _spinDuration)
            {
                if (Time.time > elapsedTime)
                {
                    elapsedTime = Time.time + 0.1f;

                    for (int i = 0; i < _slotCount; i++)
                    {
                        int randomIndex = Random.Range(0, _slotItems.Count);
                        _currentSlotValues[i] = randomIndex;
                        _slotImages[i].sprite = _slotItems[randomIndex].Sprite;
                    }
                }

                Debug.Log(Time.time + " - " + elapsedTime + " - " + _spinDuration);
            }

            _isSpinning = false;
            CheckWinCondition();
        }
        else
        {
            _popupScreen.ShowMessage("NOT ENOUGHT COINS!");
        }
        
    }

    private void CheckWinCondition()
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

        if (isWin)
        {
            _popupScreen.ShowMessage($"WIN!\n {_reward}");
            ApplicationData.Instance.GameResource[0].Count += _reward;
            var coins = ApplicationData.Instance.GameResource[0].Count;
            _coinsText.text = coins.ToString();
            Debug.Log("WIN!!!");
            return;
        }
        else
        {
            _popupScreen.ShowMessage("TRY AGAIN!");
            Debug.Log("LOSE!!!!");
            return;
        }
    }
}