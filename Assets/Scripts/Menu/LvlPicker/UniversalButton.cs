using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string btName;
    public Material UnActive;
    public Material Active;

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().material = Active;
    }


    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().material = UnActive;
    }

    private void OnMouseDown()
    {
        GlobalEvents.current.OnMenuBtClick(this, new MenuBtClickEventArgs(){ btName = this.btName});
    }
}
