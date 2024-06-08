using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] AudioClip _explosionClip;
    [SerializeField] GameObject _explosionParticle;

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.Instance.PlayOneShotSound(_explosionClip);

        if (_explosionParticle != null)
        {
            GameObject explosion = Instantiate(_explosionParticle, transform.position, transform.rotation);
            Destroy(explosion, 1f);
        }

        Destroy(gameObject);
    }
}
