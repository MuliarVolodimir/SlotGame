using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _shopButton;
    [SerializeField] Button _chooseGameButton;
    [SerializeField] Button _settingsButton;

    [SerializeField] Button _backBatton;

    [SerializeField] GameObject _menuScreen;
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _shopScreen;
    [SerializeField] GameObject _chooseGameScreen;
    [SerializeField] GameObject _settingsScreen;

    [SerializeField] TextMeshProUGUI _coinsCount;

    [SerializeField] AudioClip _aplayButtonClip;
    [SerializeField] AudioClip _cancelButtonClip;
    [SerializeField] AudioClip _backGroundClip;

    [SerializeField] private List<GameObject> _activeScreens = new List<GameObject>();
    private int _screenIndex;

    private void Start()
    {
        _mainScreen.SetActive(false);
        _settingsScreen.SetActive(false);
        _chooseGameScreen.SetActive(false);
        _shopScreen.SetActive(false);

        if (ApplicationData.Instance.FirstStart)
        {
            _menuScreen.SetActive(true);
        }
        else
        {
            _menuScreen.SetActive(false);
            _mainScreen.SetActive(true);
        }

        _startButton.onClick.AddListener(() => { Play(); });
        _shopButton.onClick.AddListener(() => { Shop(); });
        _chooseGameButton.onClick.AddListener(() => { ChooseGameButton(); });
        _settingsButton.onClick.AddListener(() => { Settings(); });

        _backBatton.onClick.AddListener(() => { CloseActiveVindow(); });

        _coinsCount.text = ApplicationData.Instance.GameResource[0].Count.ToString();

    }
    
    private void Shop()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        SwitchActive(_shopScreen);
        _activeScreens.Add(_shopScreen); 
    }

    private void Play()       
    {
        ApplicationData.Instance.FirstStart = false;
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        AudioManager.Instance.SetBackGroundMusic(_backGroundClip);
        SwitchActive(_menuScreen);
        SwitchActive(_mainScreen);
    }

    private void CloseActiveVindow()
    {
        AudioManager.Instance.PlayOneShotSound(_cancelButtonClip);

        if (_activeScreens.Count > 0)
        {
            int index = _activeScreens.Count - 1;
            _activeScreens[index].SetActive(false);
            _activeScreens.Remove(_activeScreens[index]);
        }
    }

    private void Settings()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        SwitchActive(_settingsScreen);
        _activeScreens.Add(_settingsScreen); 
    }

    private void ChooseGameButton()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        SwitchActive(_chooseGameScreen);
        _activeScreens.Add(_chooseGameScreen);
    }

    private void SwitchActive(GameObject screen)
    {
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }
}
