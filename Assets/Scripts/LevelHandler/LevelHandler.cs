using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject popUpMenu;
    private bool pause = false;


    void Start()
    {
        GlobalEvents.current.KeyDown += OnEscDown;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
