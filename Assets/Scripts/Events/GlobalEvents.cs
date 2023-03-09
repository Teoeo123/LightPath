using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents current;
    public delegate void ReciverEventEventHandler(object sender, ReciverHitEventArgs eventArgs);
    public event ReciverEventEventHandler ReciverHit;
    public event ReciverEventEventHandler ReciverEnter;
    public event ReciverEventEventHandler ReciverExit;

    // Start is called before the first frame update
    private void Awake()
    {
        current= this;
    }

    public virtual void OnReciverHit(object sender, ReciverHitEventArgs eventArgs)
    {
        if (ReciverHit != null)
        {
            ReciverHit(sender, eventArgs);
        }
    }

    public virtual void OnReciverEnter(object sender, ReciverHitEventArgs eventArgs)
    {
        if (ReciverEnter != null)
        {
            ReciverEnter(sender, eventArgs);
        }
    }

    public virtual void OnReciverExit(object sender, ReciverHitEventArgs eventArgs)
    {
        if (ReciverExit != null)
        {
            ReciverExit(sender, eventArgs);
        }
    }

}
