using System;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipMenuUI : MonoBehaviour
{
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;

    [SerializeField] AudioClip _aplayClip;

    public event Action OnExit;

    private void Start()
    {
        _yesButton.onClick.AddListener(() => { YesClick(); });
        _noButton.onClick.AddListener(() => { NoClick(); });
    }

    private void NoClick()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        Time.timeScale = 1f;
        this.gameObject.SetActive(!gameObject.activeSelf);
    }

    private void YesClick()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        Time.timeScale = 1f;
        OnExit.Invoke();
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
}
