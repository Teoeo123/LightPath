using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControler : MonoBehaviour
{
    public void GoToMainMenu()
    {
        ScenesManager.instance.LoadScene(ScenesManager.Scenes.MainMenu);

    }
}
