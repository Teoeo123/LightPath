using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;
     
public class Hover : MonoBehaviour
{
    public LineRenderer line;
    public List<ParticleSystem> particles=new List<ParticleSystem>();
    public List<Light2DBase> lights =new List<Light2DBase>();
    public TextMeshProUGUI text;
    [Range(0f, 50f)]
    public float speed;
    private bool animation = false;
    private float x, xbuf=0;
    private float y;
    private Vector3 vec = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {

        x=line.GetPosition(0).x;
        y=line.GetPosition(0).y;
        vec.y = y;
        line.enabled = false;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        foreach (ParticleSystem p in particles) p.Stop();
        foreach (Light2DBase l in lights) l.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }

    private void OnMouseEnter()
    {
        animation = true;
        Debug.Log("enter");
        line.enabled = true;
        foreach (ParticleSystem p in particles) p.Play();
        foreach (Light2DBase l in lights) l.enabled = true;
    }

    private void OnMouseExit()
    {   
        animation = false;
        //line.enabled = false;
        foreach (ParticleSystem p in particles) p.Stop();
        foreach (Light2DBase l in lights) l.enabled = false;
    }

    private void Animation()
    {
        if (animation)
        {
            if(xbuf>=x)
            {
                vec.x = xbuf;
                line.SetPosition(0, vec);
                vec.x = -xbuf;
                line.SetPosition(1, vec);
                xbuf -= speed * Time.deltaTime;
            }
            else
            {
                xbuf = x;
                vec.x = x;
                line.SetPosition(0, vec);
                vec.x = -x;
                line.SetPosition(1, vec);
            }
        }
        else
        {
            if (xbuf < 0)
            {
                vec.x = xbuf;
                line.SetPosition(0, vec);
                vec.x = -xbuf;
                line.SetPosition(1, vec);
                xbuf += speed * Time.deltaTime;
            }
            else line.enabled = false;
        }

    }
}
