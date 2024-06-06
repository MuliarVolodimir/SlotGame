using UnityEngine.SceneManagement;

public class SceneLoader
{
    private static SceneLoader _instance;

    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneLoader();
            }
            return _instance;
        }
    }

    public enum Scene
    {
        startup,
        MenuScene,
        GameScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
