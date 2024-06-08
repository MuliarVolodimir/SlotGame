using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{ 
    [SerializeField] Button _confirmButton;
    public event Action OnConfirm;

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