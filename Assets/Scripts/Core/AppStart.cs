using UnityEngine;

// this script is a simple DI container

public class AppStart : MonoBehaviour
{
    [SerializeField] Resource EconomicResoure;

    private void Start()
    {
        ApplicationData.Instance.GameResource.Add(EconomicResoure);

        DontDestroyOnLoad(this);
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.MenuScene);
    }
}
