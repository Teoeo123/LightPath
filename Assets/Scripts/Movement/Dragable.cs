using UnityEngine;


public class Dragable : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private float responsiveness;
    private float resistance;

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
        resistance = GlobalPhisicsValues.instance.resistance;
        
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
        else
            rigidBody.velocity = rigidBody.velocity*resistance;
        rigidBody.angularVelocity = rigidBody.angularVelocity*resistance;
        if(Mathf.Abs(rigidBody.velocity.x) <0.01) rigidBody.velocity.Set(rigidBody.velocity.x,0);
        if(Mathf.Abs(rigidBody.velocity.y) <0.01) rigidBody.velocity.Set(0,rigidBody.velocity.y);
        if(Mathf.Abs(rigidBody.angularVelocity) <0.01) rigidBody.angularVelocity = 0;
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
