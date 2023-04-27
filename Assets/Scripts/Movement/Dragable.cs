using UnityEngine;


public class Dragable : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private float responsiveness;

    private Vector3 windowOffset;
    private Vector3 fixPosition =Vector3.zero;
    private bool _dragable=false;
    private bool holding = true;
    private bool b=true;

    private void Start()
    {
        GlobalEvents.current.MLock += MouseLock;
        GlobalEvents.current.MUnlock+= MouseUnlock;
        responsiveness = GlobalPhisicsValues.instance.responsiveness;
        GetComponent<Rigidbody2D>().drag = GlobalPhisicsValues.instance.resistance;
        GetComponent<Rigidbody2D>().angularDrag = GlobalPhisicsValues.instance.resistance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_dragable && holding)
        {
            
            windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(b) GlobalEvents.current.OnMouseLock(this);
                b= false;
                rigidBody.velocity = (windowOffset + fixPosition - transform.position) * responsiveness;
            }
            else
            { 
                _dragable = false;
                if (!b && holding) GlobalEvents.current.OnMouseUnlock(this);
                b = true;
            }
        }

    }

    private void OnMouseOver()
    {
        if (holding)
        {
            if (!_dragable)
            {
                windowOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                fixPosition = (Vector3)transform.position - windowOffset;
            }
            _dragable = true;

        }

    }

    private void MouseLock(object sender)
    {
        if(this != sender as Dragable)
        {
            holding = false;
        }
        else holding = true;
    }

    private void MouseUnlock(object sender)
    {
        holding =true;
    }



}
