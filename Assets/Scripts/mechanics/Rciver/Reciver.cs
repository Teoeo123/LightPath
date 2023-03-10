using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reciver : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.ReciverEnter += OnLaserEnter;
        GlobalEvents.current.ReciverExit += OnLaserExit;
    }

    // Update is called once per frame
    void OnLaserEnter(object sender,ReciverHitEventArgs args)
    {
        if (index == args.laserindex)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log(this + ": enter");
        }
    }

    void OnLaserExit(object sender,ReciverHitEventArgs args)
    {
        if (index == args.laserindex)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            Debug.Log(this + ": exit");
        }
    }
}
