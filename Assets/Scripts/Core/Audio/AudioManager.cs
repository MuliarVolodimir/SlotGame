using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] GameObject _oneShootMusic;

    private ApplicationData _appData;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _appData = ApplicationData.Instance;
    }

    public void SetBackGroundMusic(AudioClip clip)
    {
        if (clip != null)
        {
            _backgroundMusic.volume = _appData.GameVolume;
            _backgroundMusic.mute = _appData.GameIsMute;
            _backgroundMusic.clip = clip;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }
    }
    
    public void PlayOneShotSound(AudioClip clip)
    {
        if (clip != null)
        {
            var oneShotMusic = Instantiate(_oneShootMusic);
            var source = oneShotMusic.GetComponent<AudioSource>();
            source.volume = _appData.GameVolume;
            source.clip = clip;
            source.loop = false;
            source.mute = _appData.GameIsMute;

            source.Play();
            DontDestroyOnLoad(oneShotMusic);
            Destroy(oneShotMusic, clip.length);
        }
    }

    public void SetVolume(float volume)
    {
        _backgroundMusic.volume = volume;
        _appData.GameVolume = volume;
    }

    public void Mute(bool isMuted)
    {
        _appData.GameIsMute = isMuted;
        _backgroundMusic.mute = isMuted;
    }
}