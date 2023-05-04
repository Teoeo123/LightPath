using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject popUpMenu;
    public GameObject endLevelMenu;
    private bool pause = false;
    public List<GameObject> listOfRecivers;
    public List<GameObject> listOfOrbs;
    private bool[] reciverSet;
    private bool[] orbSet;
    private bool endLvlAnnouncement = false;
    private float time;
    


    void Start()
    {
        reciverSet = new bool[listOfRecivers.Count];
        for(int i = 0; i < listOfRecivers.Count; i++) reciverSet[i] = false;
        orbSet= new bool[listOfOrbs.Count];
        for(int i = 0;i < listOfOrbs.Count; i++) orbSet[i] = false;
        GlobalEvents.current.KeyDown += OnEscDown;
        GlobalEvents.current.FullCharge += SetReciver;
        GlobalEvents.current.OrbEnter += OnOrbActivation;
        GlobalEvents.current.OrbExit += OnOrbDeactivation;
        time = Time.time;
    }

    void Update()
    {
        bool endGameCall=true;
        foreach(bool item in reciverSet)
        {
            if(!item) endGameCall = false;
        }
        if (endGameCall && !endLvlAnnouncement)
        {
            endLvlAnnouncement= true;
            Debug.Log("lvl end with " + CountActiveOrbs() + " orbs");
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            endLevelMenu.SetActive(true);
            GlobalEvents.current.OnLevelEnd(new LevelEndEventArgs(Time.time - time, CountActiveOrbs(), ScenesManager.Scenes.Level1));
        }

    }
    private void OnEscDown(KeyCode key)
    {
        if(key== KeyCode.Escape && !endLvlAnnouncement)
        {
            if(pause) 
            {
                pause = false;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                popUpMenu.SetActive(false);
            }
            else
            {
                pause = true;
                Time.timeScale = 0.1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                popUpMenu.SetActive(true);
            }
        }
    }

    private void SetReciver(GameObject reciver)
    {
        for(int a=0; a<listOfRecivers.Count; a++)
        {
            if (listOfRecivers[a] == reciver) reciverSet[a] = true;
        }
    }

    private void OnOrbActivation(GameObject orb)
    {
        for (int a = 0; a < listOfOrbs.Count; a++)
        {
            if (listOfOrbs[a] == orb) orbSet[a] = true;
        }
    }

    private void OnOrbDeactivation(GameObject orb)
    {
        for (int a = 0; a < listOfOrbs.Count; a++)
        {
            if (listOfOrbs[a] == orb) orbSet[a] = false;
        }
    }

    private int CountActiveOrbs()
    {
        int ret = 0;
        foreach (bool orb in orbSet) if(orb) ret++;
        return ret;
    }

}
