using UnityEngine;

public class Dragable : MonoBehaviour
{
    public bool isDragable;

    private Vector3 windowOffset;
    private Vector3 fixPosition =Vector3.zero;
    private bool _dragable=false;

    // Update is called once per frame
    void Update()
    {
        if(_dragable)
        {
            windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.Mouse0) && isDragable)
            {
                transform.position = windowOffset + fixPosition;
            }
            else _dragable = false;
        }
        
    }

    private void OnMouseOver()
    {
        if(!_dragable)
        {
            windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fixPosition = (Vector3)transform.position - windowOffset;
        }
        _dragable= true;

    }


}
