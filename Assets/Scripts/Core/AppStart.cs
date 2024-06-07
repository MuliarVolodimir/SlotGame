using UnityEngine;

// this script is a simple DI container

public class AppStart : MonoBehaviour
{
    [SerializeField] Resource EconomicResoure;

    private void Start()
    {
        ApplicationData data = new ApplicationData();
        ApplicationData.Instance.GameResource.Add(EconomicResoure);

        SceneLoader.Instance.LoadScene(SceneLoader.Scene.MenuScene);
    }
}
