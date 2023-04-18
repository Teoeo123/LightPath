using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    private bool anim=false;
    private float startPosition;
    private Vector3 bufPos;
    void Start()
    {
        GlobalEvents.current.MBtClick += OnBtActivation;
        bufPos = transform.position;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            if(transform.position.x>0)
            {
                bufPos.x -= speed * Time.deltaTime;
                transform.position = bufPos;
            }
            else
            {
                bufPos.x = 0;
                transform.position = bufPos;
            }
        }
        else
        {
            if(transform.position.x>startPosition)       
            {                                            
                bufPos.x += speed * Time.deltaTime;      
                transform.position = bufPos;             
            }                                            
            else                                         
            {                                            
                bufPos.x = startPosition;                
                transform.position= bufPos;              
            }
        }
    }

    private void OnBtActivation(object sender, MenuBtClickEventArgs args)
    {
        if(args.btName == "Levels")
        {
            anim = true;
        }
    }
}
