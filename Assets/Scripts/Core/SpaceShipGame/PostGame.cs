using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostGame : MonoBehaviour
{
    [SerializeField] Button _homeButton;
    [SerializeField] Button _restartButton;

    [SerializeField] TextMeshProUGUI _rewardText;

    private void Start()
    {
        _homeButton.onClick.AddListener(() => 
        { 
            SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainScene); 
        });
        _restartButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadScene(SceneLoader.Scene.SpaceShipGameScene);
        });
        gameObject.SetActive(false);
    }

    public void CalculateReward(int score)
    {
        int reward = score / 2;
        _rewardText.text = reward.ToString();
        ApplicationData.Instance.GameResource[0].Count += reward;
    }
}
