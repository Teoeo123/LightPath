using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateryOperation : MonoBehaviour
{
    public GameObject batteryTop;
    public GameObject batteryBottom;
    public GameObject activator;
    public Color LowBatteryColor;
    [Range(0f, 1f)]
    public float timeToDischarge;
    [Range(0f, 100f)]
    public float batteryLevel;
    private Color startColor;
    private bool on_off = false;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.MBtClick += dischargeSwitch;
        GetComponent<LineRenderer>().enabled = true;
        startColor = GetComponent<LineRenderer>().startColor;
    }
    private void Update()
    {
        if (batteryLevel >= 0)
        {
            if (on_off)
            {
                if (Time.time - time > timeToDischarge)
                {
                    batteryLevel -= 0.01f;
                    time = Time.time;
                }
            }
        }
        else GlobalEvents.current.OnBatteryDischarge(gameObject);
        if (batteryLevel < 0.4f)
        {
            GetComponent<LineRenderer>().startColor = LowBatteryColor;
            GetComponent<LineRenderer>().endColor = LowBatteryColor;
        }
        else
        {
            GetComponent<LineRenderer>().startColor = startColor;
            GetComponent<LineRenderer>().endColor = startColor;
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {     
        GetComponent<LineRenderer>().SetPosition(0, 
            batteryBottom.transform.position +(batteryTop.transform.position- batteryBottom.transform.position)*batteryLevel
            );
        GetComponent<LineRenderer>().SetPosition(1, batteryBottom.transform.position);
    }

    private void dischargeSwitch(object sender, MenuBtClickEventArgs args)
    {
        if(args.btName == activator.GetComponent<OnBt>().callName)
            on_off = !on_off;
        if(on_off) time = Time.time;
    }
}
