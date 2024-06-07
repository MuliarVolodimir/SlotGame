using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance;

    private AudioSource _audioSource;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayOnceSound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource audioSource = new AudioSource();

            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();

            Destroy(audioSource, audioSource.clip.length);
            DontDestroyOnLoad(audioSource);
        }
    }
    
    public void PlayAmbientSound(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource = new AudioSource();

            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();

            DontDestroyOnLoad(_audioSource);
        }
    }
}
