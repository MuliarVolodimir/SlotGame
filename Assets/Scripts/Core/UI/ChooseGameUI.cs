using UnityEngine;
using UnityEngine.UI;

public class ChooseGameUI : MonoBehaviour
{
    [SerializeField] Button _chestGame;
    [SerializeField] Button _spaceShipGame;

    private void Start()
    {
        _chestGame.onClick.AddListener(() => { ChestGame(); });
        _spaceShipGame.onClick.AddListener(() => { SpaceShip(); });
    }

    private void ChestGame()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.ChestGameScene);
    }

    private void SpaceShip()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.SpaceShipGameScene);
    }
}
