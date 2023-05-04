using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    public void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
    private void Awake()
    {
        instance = this; 
    }
    public enum Scenes
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
