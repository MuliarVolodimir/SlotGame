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
    [SerializeField] LoadingScreen _loadingScreen;

    [Header("Player")]
    [SerializeField] GameObject _shipPrefab;
    [SerializeField] Transform _rightShipPosition;
    [SerializeField] Transform _leftShipPosition;

    [Header("Enemy")]
    [SerializeField] float _spawnRate;
    [SerializeField] List<GameObject> _enemyPrefabs;
    [SerializeField] List<Transform> _enemySpawnPos;

    [Header("Audio")]
    [SerializeField] AudioClip _aplayClip;

    private ShipController _shipController;
    private int _score;
    private ApplicationData _appData;
    private float _nextSpawn = 0;
    private bool _gameEnd = true;
    
    private void Start()
    {
        _appData = ApplicationData.Instance;
        _moveLeft.onClick.AddListener(() => { MoveLeft(); });
        _moveRight.onClick.AddListener(() => { MoveRight(); });
        _loadingScreen.OnLoad += _loadingScreen_OnLoad; 
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void _loadingScreen_OnLoad()
    {
        MoveLeft();    
        _shipController = _shipPrefab.GetComponent<ShipController>();
        _shipController.CanShoot = true;
        _gameEnd = false;
        SpawnEnemy();
    }

    public void UpdateScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void SpaceShipGameController_OnDie()
    {
        Debug.Log("lose");
        _shipController.CanShoot = false;
        _postGame.SetActive(true);
        _postGame.GetComponent<PostGame>().CalculateReward(_score);
        _gameEnd = true;
    }

    public void MoveLeft()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _shipPrefab.transform.position = _leftShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    public void MoveRight()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _shipPrefab.transform.position = _rightShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    private void SpawnEnemy()
    {
        if (Time.time > _nextSpawn && !_gameEnd)
        {
            _nextSpawn = Time.time + _spawnRate;

            var posIndex = Random.Range(0, _enemySpawnPos.Count);
            var enemyIndex = Random.Range(0, _enemyPrefabs.Count);

            GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex], _enemySpawnPos[posIndex].position, _enemySpawnPos[posIndex].rotation);
            enemy.SetActive(true);
        }
    }
}
