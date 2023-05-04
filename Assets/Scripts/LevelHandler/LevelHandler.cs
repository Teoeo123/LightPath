using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject popUpMenu;
    private bool pause = false;
    public List<GameObject> listOfRecivers;
    public List<GameObject> listOfOrbs;
    private bool[] reciverSet;


    void Start()
    {
        reciverSet = new bool[listOfRecivers.Count];
        for(int i = 0; i < listOfRecivers.Count; i++)
            reciverSet[i] = false;
        GlobalEvents.current.KeyDown += OnEscDown;
        GlobalEvents.current.FullCharge += setReciver;
    }

    // Update is called once per frame
    void Update()
    {
        bool endGameCall=true;
        foreach(bool item in reciverSet)
        {
            if(!item) endGameCall = false;
        }
        if (endGameCall) Debug.Log("koniec");

    }
    private void OnEscDown(KeyCode key)
    {
        if(key== KeyCode.Escape)
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

    private void setReciver(GameObject reciver)
    {
        for(int a=0; a<listOfRecivers.Count; a++)
        {
            if (listOfRecivers[a] == reciver) reciverSet[a] = true;
        }
    }


}
