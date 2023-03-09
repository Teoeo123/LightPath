using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class LaserEmiter : MonoBehaviour
{
    public LineRenderer LineOfSight;
    public int index;

    public int reflections;
    public float MaxRayDistance;
    public LayerMask LayerDetection;

    private bool _reciverState = false;
    private bool _reciverLastState = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        Debug.Log("loaded", this);
    }

    // Update is called once per frame
    void Update()
    {
        LineOfSight.positionCount = 1;
        LineOfSight.SetPosition(0, transform.position);
        
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, transform.right, MaxRayDistance, LayerDetection);
        bool isMirror = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;
        Vector2 mirrorLastHitPoint = (Vector2)transform.position;
        //Debug.Log("1st normal: " + hitInfo.normal);
        //Debug.Log("1st vector: " + (hitInfo.collider ? (hitInfo.point - mirrorLastHitPoint).normalized : "null" ));
        //Debug.Log("1st reflection: " + (hitInfo.collider ? Vector2.Reflect((hitInfo.point - mirrorLastHitPoint).normalized, hitInfo.normal) : "null"));

        for (int i = 0; i < reflections; i++)
        {
            LineOfSight.positionCount += 1;

            if (hitInfo.collider != null)
            {
                LineOfSight.SetPosition(LineOfSight.positionCount - 1, hitInfo.point);
                if (hitInfo.collider.CompareTag("Mirror"))
                {
                    mirrorHitPoint= hitInfo.point;
                    mirrorHitNormal= hitInfo.normal;
                    hitInfo = Physics2D.Raycast(mirrorHitPoint, Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal), MaxRayDistance, LayerDetection);
                    if(hitInfo.collider != null) mirrorLastHitPoint= mirrorHitPoint;
                    isMirror = true;
                }
                else if(hitInfo.collider.CompareTag("Reciver"))
                {
                    GlobalEvents.current.OnReciverHit(this, new ReciverHitEventArgs() { laserindex = index });
                    _reciverState= true;
                    break;
                }
                else break;
            }
            else
            {
                if (isMirror)
                {
                    LineOfSight.SetPosition(LineOfSight.positionCount - 1, mirrorHitPoint + Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized,mirrorHitNormal) * MaxRayDistance );
                    break;
                }
                else
                {
                    LineOfSight.SetPosition(LineOfSight.positionCount - 1, transform.position + transform.right * MaxRayDistance);
                    break;
                }
            }
        }

        if (_reciverState && !_reciverLastState)
            GlobalEvents.current.OnReciverEnter(this, new ReciverHitEventArgs() { laserindex = index });
        else if (!_reciverState && _reciverLastState)
            GlobalEvents.current.OnReciverExit(this, new ReciverHitEventArgs() { laserindex = index });

        _reciverLastState = _reciverState;
        _reciverState = false;

    }

}
