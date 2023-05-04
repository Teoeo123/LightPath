using UnityEngine;

public class ButtonControler : MonoBehaviour
{
    public void GoToMainMenu()
    {
        ScenesManager.instance.LoadScene(ScenesManager.Scenes.MainMenu);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PressEscape()
    {
        GlobalEvents.current.OnKeyDown(KeyCode.Escape);
    }
}
