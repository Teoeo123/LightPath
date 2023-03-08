using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationValue;
    public float precisionRotationValue;
    public float speedRotationValue;
    [Range(0,1)]
    public float slideValue;

    private float _rotation=0;
    private bool _isrrotational;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_isrrotational )
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
        transform.Rotate(0,0,_rotation); 
        if(_rotation>0.1 || _rotation<-0.1)
            _rotation *=  slideValue;
        else
            _rotation = 0;
        //Debug.Log(_rotation);
    }

    private void OnMouseOver()
    {
        _isrrotational= true;
    }
}
