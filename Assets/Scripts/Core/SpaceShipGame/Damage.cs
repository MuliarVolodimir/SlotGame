using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] AudioClip _explosionClip;
    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.Instance.PlayOneShotSound(_explosionClip);
        Destroy(gameObject);
    }
}
