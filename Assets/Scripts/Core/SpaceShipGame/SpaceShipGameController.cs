using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipGameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject _postGame;

    [SerializeField] Button _moveLeft;
    [SerializeField] Button _moveRight;
    [SerializeField] Button _settingsButton;
    [SerializeField] GameObject _mainMenuScreen;
    [SerializeField] GameObject _settingsScreen;
    [SerializeField] Button _mainMenuButton;

    [SerializeField] TextMeshProUGUI _scoreText;
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
    public bool GameEnd = true;
    
    private void Start()
    {
        _appData = ApplicationData.Instance;
        _moveLeft.onClick.AddListener(() => { MoveLeft(); });
        _moveRight.onClick.AddListener(() => { MoveRight(); });
        _settingsButton.onClick.AddListener(() => { Settings(); });
        _mainMenuButton.onClick.AddListener(() => { MainMenu(); });

        _loadingScreen.OnLoad += _loadingScreen_OnLoad; 
        _mainMenuScreen.GetComponent<SpaceShipMenuUI>().OnExit += EndGame;
        _mainMenuScreen.SetActive(false);
        _settingsScreen.SetActive(false);
    }

    private void EndGame()
    {
        _shipController.Die();
    }

    private void MainMenu()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        Time.timeScale = 0;
        _mainMenuScreen.SetActive(!_settingsScreen.activeSelf);
    }

    private void Settings()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _settingsScreen.SetActive(!_settingsScreen.activeSelf);

        switch (_settingsScreen.activeSelf)
        {
            case true:
                Time.timeScale = 0;
                break;
            case false:
                Time.timeScale = 1f;
                break;
        }
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void _loadingScreen_OnLoad()
    {
        var tutorial = FindObjectOfType<TutorialSystem>();
        tutorial.OnConfirm += Tutorial_OnConfirm;
    }

    private void Tutorial_OnConfirm()
    {
        MoveLeft();
        _shipController = _shipPrefab.GetComponent<ShipController>();
        _shipController.CanShoot = true;
        GameEnd = false;
        SpawnEnemy();
        ApplicationData.Instance.SpaceShipFirstStart = false;
    }

    public void UpdateScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void SpaceShipGameController_OnDie()
    {
        _shipController.CanShoot = false;
        _postGame.SetActive(true);
        _postGame.GetComponent<PostGame>().CalculateReward(_score);
        GameEnd = true;
    }

    public void MoveLeft()
    {
        //AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _shipPrefab.transform.position = _leftShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    public void MoveRight()
    {
        //AudioManager.Instance.PlayOneShotSound(_aplayClip);
        _shipPrefab.transform.position = _rightShipPosition.position;
        _shipPrefab.GetComponent<ShipController>().Shoot();
    }

    private void SpawnEnemy()
    {
        if (Time.time > _nextSpawn && !GameEnd)
        {
            _nextSpawn = Time.time + _spawnRate;

            var posIndex = Random.Range(0, _enemySpawnPos.Count);
            var enemyIndex = Random.Range(0, _enemyPrefabs.Count);

            GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex], _enemySpawnPos[posIndex].position, _enemySpawnPos[posIndex].rotation);
            enemy.SetActive(true);
        }
    }
}
