using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSystem : MonoBehaviour
{
    [SerializeField] int _slotCount;
    [SerializeField] List<Sprite> _slotSprites;
    [SerializeField] List<Image> _slotImages;

    [SerializeField] PopupScreen _popup;

    private float _spinDuration = 0.1f; 

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
            int randomIndex = Random.Range(0, _slotSprites.Count);
            _currentSlotValues.Add(randomIndex);
            _slotImages[i].sprite = _slotSprites[randomIndex];
        }
    }

    public void SpinSlots()
    {
        if (_isSpinning) return;

        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        _isSpinning = true;
        float elapsedTime = 0f;

        while (elapsedTime < _spinDuration)
        {
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < _slotCount; i++)
            {
                int randomIndex = Random.Range(0, _slotSprites.Count);
                _currentSlotValues[i] = randomIndex;
                _slotImages[i].sprite = _slotSprites[randomIndex];
            }

            yield return new WaitForSeconds(0.1f);
        }

        _isSpinning = false;
        CheckWinCondition();
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
            _popup.ShowMessage("WIN!");
            Debug.Log("WIN!!!");
            return;
        }
        else
        {
            _popup.ShowMessage("LOSE!");
            Debug.Log("LOSE!!!!");
            return;
        }
    }
}