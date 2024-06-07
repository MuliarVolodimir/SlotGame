using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletSpawnPos;
    [SerializeField] float _fireRate;

    public bool CanShoot = false;

    private float _nextFire;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Die();
    }

    private void Die()
    {
        FindAnyObjectByType<SpaceShipGameController>().SpaceShipGameController_OnDie();
        gameObject.SetActive(false);
    }

    public void Shoot()
    {
        if (Time.time > _nextFire && CanShoot)
        {
            _nextFire = Time.time + _fireRate;
            GameObject bullet = Instantiate(_bullet, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet.SetActive(true);
        }  
    }
}