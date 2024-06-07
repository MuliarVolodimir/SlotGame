using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipGameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject _postGame;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] Button _moveLeft;
    [SerializeField] Button _moveRight;

    [Header("Player")]
    [SerializeField] GameObject _shipPrefab;
    [SerializeField] Transform _rightShipPosition;
    [SerializeField] Transform _leftShipPosition;

    [Header("Enemy")]
    [SerializeField] float _spawnRate;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] List<Transform> _enemySpawnPos;

    private int _score;
    private ApplicationData _appData;
    private float _nextSpawn;
    
    private void Start()
    {
        _appData = ApplicationData.Instance;
        _shipPrefab.GetComponent<ShipController>().OnDie += SpaceShipGameController_OnDie;

        _moveLeft.onClick.AddListener(() => { MoveLeft(); });
        _moveRight.onClick.AddListener(() => { MoveRight(); });
        MoveLeft();
    }

    private void SpaceShipGameController_OnDie()
    {
        //system to respawn or go to main menu
        _postGame.SetActive(true);
    }

    private void Update()
    {
        SpawnEnemy();
    }

    public void MoveLeft()
    {
        _shipPrefab.transform.position = _leftShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    public void MoveRight()
    {
        _shipPrefab.transform.position = _rightShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    private void SpawnEnemy()
    {
        if (Time.time > _nextSpawn)
        {
            _nextSpawn = Time.time * _spawnRate;

            var index = Random.Range(0, _enemySpawnPos.Count);
            GameObject enemy = Instantiate(_enemyPrefab, _enemySpawnPos[index].position, _enemySpawnPos[index].rotation);
            enemy.SetActive(true);
        }
    }
}
