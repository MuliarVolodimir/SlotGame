using UnityEngine;
using UnityEngine.UI;

public class ChestGameUI : MonoBehaviour
{
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] GameObject _settingsScreen;

    [SerializeField] AudioClip _aplayButtonClip;
    [SerializeField] AudioClip _backClip;

    private void Start()
    {
        _mainMenuButton.onClick.AddListener(() => { MainMenu(); });
        _settingsButton.onClick.AddListener(() => { Settings(_settingsScreen); });
        _settingsScreen.SetActive(false);
    }

    private void MainMenu()
    {
        AudioManager.Instance.PlayOneShotSound(_backClip);
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainScene);
    }

    private void Settings(GameObject screen)
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }
}
