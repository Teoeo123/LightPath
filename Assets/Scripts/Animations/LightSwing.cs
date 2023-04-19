using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwing : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float swingRange;
    [Range(0.0f, 360.0f)]
    public float offset;
    [Range(0.0f, 2.0f)]
    public float speed;
    [Range(0.0f, 3.14f)]
    public float startOffset;

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin( Time.time * speed + startOffset)*swingRange + offset;
        Vector3 rot= Vector3.zero;
        rot.z = sin;
        transform.eulerAngles= rot;
        

    }
}
