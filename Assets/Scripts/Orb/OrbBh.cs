using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OrbBh : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;
    private bool messageSended = false;
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
            
            if (!messageSended)
            {
                GlobalEvents.current.OnOrbEnter(gameObject);
                messageSended = true;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Light2D>().color = Color.white;
            
            if (messageSended)
            {
                GlobalEvents.current.OnOrbExit(gameObject);
                messageSended = false;
            }
        }


    }

    private void OnOrbHit(object sender, ReciverHitEventArgs args)
    {
        if(args.hitobject == gameObject)
        {
            active = true;
        }
    }
}
