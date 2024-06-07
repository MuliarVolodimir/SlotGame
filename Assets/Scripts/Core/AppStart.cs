using UnityEngine;

public class AppStart : MonoBehaviour
{
    [SerializeField] Resource EconomicResoure;

    private void Start()
    {
        ApplicationData data = new ApplicationData();
        ApplicationData.Instance.GameResource.Add(EconomicResoure);

        SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainScene);
    }
}
