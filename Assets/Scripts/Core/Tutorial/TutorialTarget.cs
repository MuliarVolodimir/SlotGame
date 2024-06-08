using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTarget : MonoBehaviour
{
    [SerializeField] Button _targetbutton;
    [SerializeField] GameObject _tutorialScreen;
    [SerializeField] LoadingScreen _loadingScreen;

    [SerializeField] bool _tutorialOnStart = false;

    void Start()
    {
        if (!ApplicationData.Instance.FirstStart())
        {
            Destroy(gameObject);
            return;
        }

        _loadingScreen.OnLoad += _loadingScreen_OnLoad;
    }

    private void _loadingScreen_OnLoad()
    {
        if (_tutorialScreen != null)
        {
            if (!_tutorialOnStart)
            {
                _targetbutton.onClick.AddListener(() => { ShowTutorial(); });
            }
            else
            {
                ShowTutorial();
            }
        }
    }

    private void ShowTutorial()
    {
        Debug.Log("active");
        _tutorialScreen.SetActive(true);
        Destroy(gameObject);
    }
}
