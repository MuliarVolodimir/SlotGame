using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{ 
    [SerializeField] Button _confirmButton;
    [SerializeField] Tutorials _tutorialType;

    public event Action OnConfirm;

    private enum Tutorials
    {
        MainScene,
        Chest,
        SpaceShip
    }

    void Start()
    {
        if (!ApplicationData.Instance.FirstStart())
        {
            Destroy(gameObject);
            return;
        }

        _confirmButton.onClick.AddListener(() => { HideTutorial(); });
        gameObject.SetActive(false);
    }

    private void HideTutorial()
    {
        OnConfirm?.Invoke();
        Destroy(gameObject);
    }
}