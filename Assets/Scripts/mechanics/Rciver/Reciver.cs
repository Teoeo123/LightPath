
using UnityEngine;

public class Reciver : MonoBehaviour
{
    public GameObject thisObject;
    public int index;
    public Color targetColor;
    public float speedOfCharging=0.5f;
    private Color startingColor;
    private Gradient grad;
    private float charge;
    float lastCharge;
    // Start is called before the first frame update
    void Start()
    {
        startingColor = GetComponent<SpriteRenderer>().color;

        grad = new Gradient()
        {
                colorKeys = new GradientColorKey[]
                {
                    new GradientColorKey()
                    {
                        color=startingColor,
                        time = 0f
                    },
                    new GradientColorKey()
                    {
                        color=targetColor,
                        time = 1f
                    }
                },

                alphaKeys = new GradientAlphaKey[] 
                { 
                    new GradientAlphaKey() 
                    { 
                        alpha = 1f, 
                        time = 0f 
                    }, 
                    new GradientAlphaKey() 
                    { 
                        alpha = 1f, 
                        time = 1f 
                    } 
                }
            };

        //GlobalEvents.current.ReciverEnter += OnLaserEnter;
        //GlobalEvents.current.ReciverExit += OnLaserExit;
        GlobalEvents.current.ReciverHit += OnLaserHit;
        lastCharge= Time.time;
    }
    private void Update()
    {
        GetComponent<SpriteRenderer>().color = grad.Evaluate(charge);
    }

    // Update is called once per frame
    void OnLaserEnter(object sender,ReciverHitEventArgs args)
    {
        if (index == args.laserindex && args.hitobject == thisObject)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log(this + ": enter");
        }
    }

    void OnLaserExit(object sender,ReciverHitEventArgs args)
    {
        if (index == args.laserindex && args.hitobject == thisObject)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log(this + ": exit");
        }
    }
    void OnLaserHit(object sender, ReciverHitEventArgs args)
    {
        if (index == args.laserindex && args.hitobject == thisObject)
        {

            if (Time.time - lastCharge > speedOfCharging && charge<1f) charge += 0.01f;
            else if(charge>1f) charge=1f;
        }
    }
}
