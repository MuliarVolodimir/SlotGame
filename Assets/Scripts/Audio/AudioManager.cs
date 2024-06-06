using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance;

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

    public void PlaySound(AudioClip clip, bool isLoop)
    {
        if (clip != null)
        {
            AudioSource audioSource = new AudioSource();

            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.Play();

            DontDestroyOnLoad(audioSource);
            Destroy(audioSource, audioSource.clip.length);
        }
    }
}
