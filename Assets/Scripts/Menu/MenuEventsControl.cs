using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventsControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menuScreen;
    public GameObject levelsScreen;
    public float swapSpeed;
    private Vector3 velo;
    private bool isMenuActive = true;

    void Start()
    {
        GlobalEvents.current.MBtClick += ButtonsControl;
        velo = new Vector3(swapSpeed, 0, 0);
    }

    private void Update()
    {
        if (isMenuActive)
        {
            if (menuScreen.transform.position.x < 0)
            {
                menuScreen.transform.position += velo * Time.deltaTime;
                levelsScreen.transform.position += velo * Time.deltaTime;
            }
        }
        else
        {
            if (levelsScreen.transform.position.x > 0)
            {
                levelsScreen.transform.position -= velo * Time.deltaTime;
                menuScreen.transform.position -= velo * Time.deltaTime;
            }
        }
    }
    public void ButtonsControl(object sender, MenuBtClickEventArgs args)
    {
        if (args.btName == "Levels")
        {
            isMenuActive = false;
        }
        else if (args.btName == "Menu")
        {
            isMenuActive = true;
        }
        else if(args.btName == "Exit")
        {
            Application.Quit();
        }
    }
}
