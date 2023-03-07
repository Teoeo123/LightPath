using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Dragable : MonoBehaviour
{
    public bool isDragable;
    private Vector3 windowOffset;


    // Update is called once per frame
    void Update()
    {
        windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        windowOffset.z = 0;
        
        if (Input.GetKey(KeyCode.Mouse0) && isDragable)
        {
            
            transform.position = windowOffset;
        }

    }
}
