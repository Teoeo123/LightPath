using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmiter : MonoBehaviour
{
    public LineRenderer LineOfSight;

    public int reflections;
    public float MaxRayDistance;
    public LayerMask LayerDetection;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
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

    }
}
