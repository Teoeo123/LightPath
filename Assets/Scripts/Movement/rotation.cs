using System;
using UnityEngine;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationValue;
    public float precisionRotationValue;
    public float speedRotationValue;
    public Rigidbody2D rigidBody;
    [Range(0,1)]
    public float slideValue;

    private bool pause = false;
    private float _rotation=0;
    private bool _isrrotational;
    void Start()
    {
        GlobalEvents.current.Pause += OnPause;
        GlobalEvents.current.Continue += OnContinue;
        rigidBody.angularDrag = GlobalPhisicsValues.instance.resistance;
    }

    // Update is called once per frame
    void Update()
    {
        _rotation *= slideValue;
        if (Mathf.Abs(_rotation)< 0.001f) _rotation= 0;
        if(_isrrotational && !pause )
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                if(Input.GetKey(KeyCode.LeftShift))
                    _rotation += precisionRotationValue;
                else if (Input.GetKey(KeyCode.LeftControl))
                    _rotation += speedRotationValue;
                else
                    _rotation += rotationValue;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    _rotation -= precisionRotationValue;
                else if (Input.GetKey(KeyCode.LeftControl))
                    _rotation -= speedRotationValue;
                else
                    _rotation -= rotationValue;
            }
            else _isrrotational= false;
        }
        rigidBody.angularVelocity = _rotation;

    }

    private void OnMouseOver()
    {
        _isrrotational= true;
    }

    private void OnPause()
    {
        pause = true;
    }

    private void OnContinue()
    {
        pause = false;
    }

}
