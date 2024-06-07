using UnityEngine;
using UnityEngine.UI;

public class ChooseGameUI : MonoBehaviour
{
    [SerializeField] Button _chestGame;
    [SerializeField] Button _spaceShipGame;

    [SerializeField] AudioClip _aplayButtonClip;

    private void Start()
    {
        _chestGame.onClick.AddListener(() => { ChestGame(); });
        _spaceShipGame.onClick.AddListener(() => { SpaceShip(); });
    }

    private void ChestGame()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.ChestGameScene);
    }

    private void SpaceShip()
    {
        AudioManager.Instance.PlayOneShotSound(_aplayButtonClip);
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.SpaceShipGameScene);
    }
}
