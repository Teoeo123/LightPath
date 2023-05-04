
using UnityEngine;

public class UniversalButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string btName;
    public Material UnActive;
    public Material Active;

    private bool pause=false;
    private void Start()
    {
        GlobalEvents.current.Pause += OnPause;
        GlobalEvents.current.Continue += OnContinue;
    }

    private void OnMouseEnter()
    {
        if(!pause) GetComponent<SpriteRenderer>().material = Active;
    }


    private void OnMouseExit()
    {
        if (!pause) GetComponent<SpriteRenderer>().material = UnActive;
    }

    private void OnMouseDown()
    {
        if (!pause) GlobalEvents.current.OnMenuBtClick(this, new MenuBtClickEventArgs(){ btName = this.btName});
    }
    private void OnPause()
    {
        pause = true;
    }

    private void OnContinue()
    {
        pause = false;
    }

}
