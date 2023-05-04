using UnityEngine;

public class OnBt : MonoBehaviour
{

    public Color colorON;
    public Color colorOFF;
    public string callName;

    private bool on_off=false;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.MBtClick += colorChanger;
    }

    private void Update()
    {
        if (on_off) GetComponent<SpriteRenderer>().color= colorON;
        else GetComponent<SpriteRenderer>().color= colorOFF;
    }

    void colorChanger(object sender, MenuBtClickEventArgs args)
    {
        if (callName== args.btName)
            on_off = !on_off;
    }

}
