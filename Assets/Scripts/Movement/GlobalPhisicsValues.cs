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
    [Range(0f, 1f)]
    public float resistance = 0.973f;
}
