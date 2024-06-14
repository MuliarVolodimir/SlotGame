using UnityEngine;

public class AppStart : MonoBehaviour
{
    private const string COINS_KEY = "coins";
    [SerializeField] Resource EconomicResoure;

    private void Start()
    {
        LoadData();
        SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainScene);
        DontDestroyOnLoad(gameObject);
    }

    private void LoadData()
    {
        ApplicationData data = new ApplicationData();
        ApplicationData.Instance.GameResource.Add(EconomicResoure);

        if (PlayerPrefs.HasKey(COINS_KEY))
        {
            //Debug.Log(PlayerPrefs.GetInt(COINS_KEY));
            ApplicationData.Instance.GameResource[0].Count = PlayerPrefs.GetInt(COINS_KEY);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(COINS_KEY, ApplicationData.Instance.GameResource[0].Count);
        PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetInt(COINS_KEY));
    }
}
