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
        //chest game
    }

    private void SpaceShip()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.GameScene);
    }
}
