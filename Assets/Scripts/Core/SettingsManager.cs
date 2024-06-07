using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Slider _volumeSlider;
    [SerializeField] Button _muteButton;
    [SerializeField] AudioClip _applayClip;

    private ApplicationData _appData;

    private void Start()
    {
        _appData = ApplicationData.Instance;

        _muteButton.onClick.AddListener(ToggleMute);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _volumeSlider.value = _appData.GameVolume;
    }

    private void SetVolume(float volume)
    {
        if (!_appData.GameIsMute)
        {
            AudioManager.Instance.SetVolume(volume);
            _appData.GameVolume = volume;
        }
    }

    private void ToggleMute()
    {
        AudioManager.Instance.PlayOneShotSound(_applayClip);
        _appData.GameIsMute = !_appData.GameIsMute;
        AudioManager.Instance.Mute(_appData.GameIsMute);
    }
}
