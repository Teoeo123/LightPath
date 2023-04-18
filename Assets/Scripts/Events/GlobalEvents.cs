using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents current;

    public delegate void ReciverEventEventHandler(object sender, ReciverHitEventArgs eventArgs);
    public event ReciverEventEventHandler ReciverHit;
    public event ReciverEventEventHandler ReciverEnter;
    public event ReciverEventEventHandler ReciverExit;

    public delegate void MenuBtClick(object sender, MenuBtClickEventArgs eventArgs);
    public event MenuBtClick MBtClick;

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

    public virtual void OnMenuBtClick(object sender, MenuBtClickEventArgs eventArgs )
    {
        if(MBtClick != null)
        {
            MBtClick(sender, eventArgs);
        }
    }

}
