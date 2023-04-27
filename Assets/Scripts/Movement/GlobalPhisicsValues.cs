using System;
using UnityEngine;

public class GlobalPhisicsValues : MonoBehaviour
{
    public static GlobalPhisicsValues instance;
    private void Awake()
    {
        instance = this; 
    }


    public float responsiveness = 2;
    [Range(0f, 10f)]
    public float resistance = 5;
}
