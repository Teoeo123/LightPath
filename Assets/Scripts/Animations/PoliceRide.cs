using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class PoliceRide : MonoBehaviour
{
    public float Hight;
    public float RideStart;
    public float RideEnd;
    [Range(0f, 500f)]
    public float speedOfLight;
    [Range(0, 100)]
    public int probability;
    public float MinBreakTime;
    public float speedOfRide;
    private Vector3 startPos;
    private Vector3 bufPosition;
    private float timeOffset;
    private bool animplay;
    private System.Random random = new System.Random();

    private void Start()
    {
        bufPosition= transform.position;
        bufPosition.y=Hight;
        bufPosition.x=RideStart;
        startPos = transform.position = bufPosition;
        timeOffset = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        IsAnimRedyToPlay();
        Anim();
    }

    private void Anim()
    {
        if (animplay)
        {
            transform.Rotate(0, 0, Time.deltaTime * speedOfLight);
            if (transform.position.x < RideEnd)
            {
                bufPosition.x += speedOfRide * Time.deltaTime;
                transform.position = bufPosition;
            }
            else
            {
                transform.position = startPos;
                bufPosition = startPos;
                animplay = false;
            }
            timeOffset = Time.time;
        }
    }

    private void IsAnimRedyToPlay()
    {
        if (!animplay)
        {
            if (Time.time - timeOffset > MinBreakTime)
            {
                int d = random.Next() % 100;
                if (d < probability)
                {
                    animplay = true;
                }
                    timeOffset= Time.time;
            }
        }
    }
    
}
