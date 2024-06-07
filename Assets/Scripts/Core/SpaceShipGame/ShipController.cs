using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletSpawnPos;
    [SerializeField] float _fireRate;

    private float _nextFire;
    public event Action OnDie;

    public void Die()
    {
        Destroy(this);
        OnDie?.Invoke();
    }

    public void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            GameObject bullet = Instantiate(_bullet, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet.SetActive(true);
        }  
    }
}