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

    [SerializeField] Button _backButton;

    [SerializeField] GameObject _menuScreen;
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _shopScreen;
    [SerializeField] GameObject _chooseGameScreen;
    [SerializeField] GameObject _settingsScreen;

    [SerializeField] TextMeshProUGUI _coinsCount;

    [SerializeField] AudioClip _playButtonClip;
    [SerializeField] AudioClip _cancelButtonClip;
    [SerializeField] AudioClip _backgroundClip;

    private List<GameObject> _activeScreens;
    private int _screenIndex = -1;

    private void Start()
    {
        _activeScreens = new List<GameObject> { _menuScreen, _mainScreen, _shopScreen, _chooseGameScreen, _settingsScreen };

        foreach (var screen in _activeScreens)
        {
            screen.SetActive(false);
        }

       

        if (ApplicationData.Instance.FirstStart)
        {
            _menuScreen.SetActive(true);
            _coinsCount.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(false);
            _screenIndex = _activeScreens.IndexOf(_menuScreen);
        }
        else
        {
            _mainScreen.SetActive(true);
            _coinsCount.gameObject.SetActive(true);
            _backButton.gameObject.SetActive(true);
            _screenIndex = _activeScreens.IndexOf(_mainScreen);
        }

        _startButton.onClick.AddListener(Play);
        _shopButton.onClick.AddListener(Shop);
        _chooseGameButton.onClick.AddListener(ChooseGame);
        _settingsButton.onClick.AddListener(ToggleSettings);
        _backButton.onClick.AddListener(CloseActiveWindow);

        _coinsCount.text = ApplicationData.Instance.GameResource[0].Count.ToString();
    }

    private void Shop()
    {
        AudioManager.Instance.PlayOneShotSound(_playButtonClip);
        SwitchActive(_shopScreen);
    }

    private void Play()
    {
        ApplicationData.Instance.FirstStart = false;
        _coinsCount.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);

        AudioManager.Instance.PlayOneShotSound(_playButtonClip);
        AudioManager.Instance.SetBackGroundMusic(_backgroundClip);
        SwitchActive(_mainScreen);
    }

    private void CloseActiveWindow()
    {
        AudioManager.Instance.PlayOneShotSound(_cancelButtonClip);

        if (_activeScreens[_screenIndex] != _mainScreen)
        {
            _activeScreens[_screenIndex].SetActive(false);
            _mainScreen.SetActive(true);
            _screenIndex = _activeScreens.IndexOf(_mainScreen);
        }
    }

    private void ToggleSettings()
    {
        AudioManager.Instance.PlayOneShotSound(_playButtonClip);

        _settingsScreen.SetActive(!_settingsScreen.activeSelf);
    }

    private void ChooseGame()
    {
        AudioManager.Instance.PlayOneShotSound(_playButtonClip);
        SwitchActive(_chooseGameScreen);
    }

    private void SwitchActive(GameObject screen)
    {
        if (screen != null)
        {
            foreach (var activeScreen in _activeScreens)
            {
                activeScreen.SetActive(false);
            }

            screen.SetActive(true);
            _screenIndex = _activeScreens.IndexOf(screen);
        }
    }
}
