using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Active : MonoBehaviour
{
    public SpriteRenderer efector;
    public Material ActiveSh;
    private Material UnactiveSh;
    bool over;
    // Start is called before the first frame update
    void Start()
    {
        UnactiveSh = efector.material;
    }

    private void OnMouseOver()
    {
        if(efector.material != ActiveSh)
        {
            efector.material = ActiveSh;
            over= true;
        }
    }

    private void OnMouseUp()
    {
        if (!over) efector.material = UnactiveSh; 
    }

    private void OnMouseExit()
    {
        if(!Input.GetKey(KeyCode.Mouse0))
            efector.material = UnactiveSh;
        over= false;
    }
}
