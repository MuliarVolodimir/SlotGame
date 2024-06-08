using System;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipMenuUI : MonoBehaviour
{
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;

    public event Action OnExit;

    private void Start()
    {
        _yesButton.onClick.AddListener(() => { YesClick(); });
        _noButton.onClick.AddListener(() => { NoClick(); });
    }

    private void NoClick()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(!gameObject.activeSelf);
    }

    private void YesClick()
    {
        Time.timeScale = 1f;
        OnExit.Invoke();
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
}
