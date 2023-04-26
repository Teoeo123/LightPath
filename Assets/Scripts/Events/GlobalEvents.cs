using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents current;
    private void Awake()
    {
        current = this;
    }
    //ReciverEvents
    public delegate void ReciverEventEventHandler(object sender, ReciverHitEventArgs eventArgs);
    public event ReciverEventEventHandler ReciverHit;
    public event ReciverEventEventHandler ReciverEnter;
    public event ReciverEventEventHandler ReciverExit;
    public event ReciverEventEventHandler OrbHit;

    public virtual void OnOrbHit(object sender, ReciverHitEventArgs eventArgs)
    {
        if (OrbHit != null)
        {
            OrbHit(sender, eventArgs);
        }
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

    //MenuEvents
    public delegate void MenuBtClick(object sender, MenuBtClickEventArgs eventArgs);
    public event MenuBtClick MBtClick;

    public virtual void OnMenuBtClick(object sender, MenuBtClickEventArgs eventArgs)
    {
        if (MBtClick != null)
        {
            MBtClick(sender, eventArgs);
        }
    }

    //MousDragInGameEvents
    public delegate void MouseDrag(object sender);
    public event MouseDrag MLock;
    public event MouseDrag MUnlock;

    public virtual void OnMouseLock(object sender)
    {
        if (MLock != null)
        {
            MLock(sender);
        }
    }

    public virtual void OnMouseUnlock(object sender)
    {
        if (MUnlock != null)
        {
            MUnlock(sender);
        }
    }

    //BatteryEvent
    public delegate void BatteryLvl(GameObject sender);
    public event BatteryLvl BatteryDischarge;
    public void OnBatteryDischarge(GameObject sender)
    {
        if(BatteryDischarge!=null) BatteryDischarge(sender);
    }

}
