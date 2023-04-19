using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.U2D;

public class PoliceRide : MonoBehaviour
{
    public Light2DBase Light1;
    public Light2DBase Light2;
    public float Hight;
    public float RideStart;
    public float RideEnd;
    public float speedOfLight;
    public float speedOfRide;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().intensity = Mathf.Sin(Time.time * speedOfLight);
    }
}
