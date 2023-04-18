using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.MBtClick += HideMainMenu;
    }

    // Update is called once per frame
    public void HideMainMenu(object sender, MenuBtClickEventArgs args)
    {
        gameObject.SetActive(false);
    }
}
