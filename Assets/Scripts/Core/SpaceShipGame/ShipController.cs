using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float _fireRate;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletSpawnPos;

    [SerializeField] AudioClip _shipShootClip;
    [SerializeField] AudioClip _explosionClip;
    [SerializeField] GameObject _explosionParticle;

    public bool CanShoot = false;
    private float _nextFire;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        Die();
    }

    public void Die()
    {
        AudioManager.Instance.PlayOneShotSound(_explosionClip);
        
        GameObject explosion = Instantiate(_explosionParticle, transform.position, transform.rotation);
        Destroy(explosion, 1f);

        var controller = FindObjectOfType<SpaceShipGameController>();

        if (!controller.GameEnd)
        {
            controller.SpaceShipGameController_OnDie();
        }
        gameObject.SetActive(false);
    }

    public void Shoot()
    {
        if (Time.time > _nextFire && CanShoot)
        {
            AudioManager.Instance.PlayOneShotSound(_shipShootClip);
            _nextFire = Time.time + _fireRate;
            GameObject bullet = Instantiate(_bullet, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet.SetActive(true);
        }  
    }
}