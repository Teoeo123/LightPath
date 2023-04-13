using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightSwing : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float swingRange;
    [Range(0.0f, 360.0f)]
    public float offset;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin( Time.time * speed )*swingRange + offset;
        Vector3 rot= Vector3.zero;
        rot.z = sin;
        transform.eulerAngles= rot;
        

    }
}
