using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupScreen : MonoBehaviour
{
    [SerializeField] Button _confirmButton;
    [SerializeField] TextMeshProUGUI _messageText;

    public event Action OnConfirm;

    private void Start()
    {
        gameObject.SetActive(false);
        _confirmButton.onClick.AddListener(() => { Close(); });
    }

    private void Close()
    {
        OnConfirm?.Invoke();
        gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        gameObject.SetActive(true);
        _messageText.text = message;
    }
}
