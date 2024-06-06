using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button _spinButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _chooseGameButton;

    [SerializeField] GameObject _slotScreen;
    [SerializeField] GameObject _chooseGameScreen;
    [SerializeField] GameObject _settingsScreen;

    [Header("Audio")]
    [SerializeField] AudioClip _aplayButtonClip;
    [SerializeField] AudioClip _cancelButtonClip;

    [SerializeField] SlotSystem _slotSystem;
    [SerializeField] AppStart _container;

    private void Start()
    {
        _container = FindAnyObjectByType<AppStart>();

        Hide();

        _spinButton.onClick.AddListener(() => { Spin(); });
        _settingsButton.onClick.AddListener(() => { Settings(); });
        _chooseGameButton.onClick.AddListener(() => { ChooseGameButton(); });
    }

    private void Settings()
    {
        SwitchActive(_settingsScreen);
    }

    private void ChooseGameButton()
    {
        SwitchActive(_chooseGameScreen);
    }

    private void Spin()
    {
        Hide();
        _slotSystem.SpinSlots();
    }

    private void Hide()
    {
        _settingsScreen.active = false;
        //_chooseGameScreen.active = false;
    }

    private void SwitchActive(GameObject screen)
    {
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }
}
