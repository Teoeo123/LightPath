using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Dragable : MonoBehaviour
{
    public bool isDragable;

    private Vector2 windowOffset;
    private Vector2 fixPosition;
    private bool _dragable=false;
    MeshRenderer m_Renderer;
    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_dragable)
        {
            windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.Mouse0) && isDragable)
            {
                transform.position = windowOffset + fixPosition;
                Debug.Log("fix pos: " + (windowOffset + fixPosition) );
            }
            else _dragable = false;
        }
        
    }

    private void OnMouseOver()
    {
        if(!_dragable)
        {
            windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fixPosition = (Vector2)transform.position - windowOffset;
        }
        _dragable= true;

    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseEnter()
    {

    }

}
