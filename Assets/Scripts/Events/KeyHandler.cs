using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GlobalEvents.current.OnKeyDown(KeyCode.Escape); 
        if (Input.GetKeyUp(KeyCode.Escape))  
            GlobalEvents.current.OnKeyUp(KeyCode.Escape);
        if (Input.GetKey(KeyCode.Escape))  
            GlobalEvents.current.OnKey(KeyCode.Escape); 
    }
}
