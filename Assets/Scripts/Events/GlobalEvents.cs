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
        OrbHit?.Invoke(sender, eventArgs);
    }

    public virtual void OnReciverHit(object sender, ReciverHitEventArgs eventArgs)
    {
        ReciverHit?.Invoke(sender, eventArgs);
    }

    public virtual void OnReciverEnter(object sender, ReciverHitEventArgs eventArgs)
    {
        ReciverEnter?.Invoke(sender, eventArgs);
    }

    public virtual void OnReciverExit(object sender, ReciverHitEventArgs eventArgs)
    {
        ReciverExit?.Invoke(sender, eventArgs);
    }

    //MenuEvents
    public delegate void MenuBtClick(object sender, MenuBtClickEventArgs eventArgs);
    public event MenuBtClick MBtClick;

    public virtual void OnMenuBtClick(object sender, MenuBtClickEventArgs eventArgs)
    {
        MBtClick?.Invoke(sender, eventArgs);
    }

    //MousDragInGameEvents
    public delegate void MouseDrag(object sender);
    public event MouseDrag MLock;
    public event MouseDrag MUnlock;

    public virtual void OnMouseLock(object sender)
    {
        MLock?.Invoke(sender);
    }

    public virtual void OnMouseUnlock(object sender)
    {
        MUnlock?.Invoke(sender);
    }

    //BatteryEvent
    public delegate void BatteryLvl(GameObject sender);
    public event BatteryLvl BatteryDischarge;
    public virtual void OnBatteryDischarge(GameObject sender)
    {
        BatteryDischarge?.Invoke(sender);
    }

    //KeyEvents
    public delegate void KeyHandler(KeyCode key);
    public event KeyHandler KeyDown;
    public event KeyHandler KeyUp;
    public event KeyHandler Key;

    public virtual void OnKeyDown(KeyCode key)
    {
        KeyDown?.Invoke(key);
    }
    public virtual void OnKeyUp(KeyCode key)
    {
        KeyUp?.Invoke(key);
    }
    public virtual void OnKey(KeyCode key)
    {
        Key?.Invoke(key);
    }
    //Pause
    public delegate void Empty();
    public event Empty Pause;
    public event Empty Continue;

    public virtual void OnGamePause()
    {
        Pause?.Invoke();
    }
    public virtual void OnGameReturn()
    {
        Continue?.Invoke();
    }
    //End of Game
    public delegate void EndGameCall(GameObject sender);
    public event EndGameCall FullCharge;
    public event EndGameCall OrbEnter;
    public event EndGameCall OrbExit;

    public virtual void OnFullCharge(GameObject sender)
    {
        FullCharge?.Invoke(sender);
    }

    public virtual void OnOrbEnter(GameObject sender)
    {
        OrbEnter?.Invoke(sender);
    }

    public virtual void OnOrbExit(GameObject sender)
    {
        OrbExit?.Invoke(sender);
    }

    public delegate void EndGameCallWithDetils(LevelEndEventArgs args);
    public event EndGameCallWithDetils LevelEnd;

    public virtual void OnLevelEnd(LevelEndEventArgs args)
    { 
        LevelEnd?.Invoke(args); 
    }

}
