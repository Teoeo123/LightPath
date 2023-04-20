using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;

public class OrbBh : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;
    void Start()
    {
        GlobalEvents.current.OrbHit += OnOrbHit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        active = false;
    }

    private void LateUpdate()
    {
        if (active)
        {
            GetComponent<SpriteRenderer>().color= Color.green;
            GetComponent<Light2D>().color= Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Light2D>().color = Color.white;
        }
    }

    private void OnOrbHit(object sender, ReciverHitEventArgs args)
    {
        Debug.Log("hit");
        if(args.hitobject == gameObject)
        {
            active = true;
        }
    }
}
