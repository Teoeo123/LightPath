
using System;
using Unity.VisualScripting;
using UnityEngine;

public class LaserEmiter : MonoBehaviour
{
    public LineRenderer LineOfSight;
    public int index;
    public double refractionFactor;
    public int reflections;
    public float MaxRayDistance;
    public LayerMask LayerDetection;
    [Range(1.0f, 2.0f)]
    public float lightTransmittingDencity;
    
    private bool _reciverState = false;
    private bool _reciverLastState = false;
    private GameObject _reciver;

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
        bool colision = false;
        bool insideGlassState = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;
        Vector2 outputDirection = Vector2.zero;
        Vector2 mirrorLastHitPoint = (Vector2)transform.position;


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
                    outputDirection = Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal);
                    hitInfo = Physics2D.Raycast(mirrorHitPoint,outputDirection , MaxRayDistance, LayerDetection);
                    if(hitInfo.collider != null) mirrorLastHitPoint= mirrorHitPoint;
                    colision = true; 
                }
                else if(hitInfo.collider.CompareTag("Reciver"))
                {
                    _reciver = hitInfo.collider.GameObject();
                    GlobalEvents.current.OnReciverHit(this, new ReciverHitEventArgs() { laserindex = index, hitobject=_reciver });
                    _reciverState= true;
                    break;
                }
                else if(hitInfo.collider.CompareTag("Glass"))
                {
                    colision = true;
                    mirrorHitPoint = hitInfo.point;
                    mirrorHitNormal = hitInfo.normal;
                    Vector2 arrivalDirection = (mirrorHitPoint - mirrorLastHitPoint).normalized;
                    double Alpha = Vector2.SignedAngle(mirrorHitNormal, arrivalDirection)+180;
                    double AlphaRad = Math.PI * Alpha / 180.0;
                    float sinBeta = (insideGlassState)? (float)Math.Sin(AlphaRad) / (float)Math.Pow(lightTransmittingDencity,-1) : (float)Math.Sin(AlphaRad) / lightTransmittingDencity;
                    if (Math.Abs(sinBeta) > 1)
                    {
                        outputDirection = Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal);
                        hitInfo = Physics2D.Raycast(mirrorHitPoint + outputDirection.normalized / 100, outputDirection, MaxRayDistance, LayerDetection);
                    }
                    else
                    {
                        float cosBeta = (float)Math.Sqrt(1 - Math.Pow(sinBeta, 2));
                        insideGlassState = !insideGlassState;
                        if (Alpha > 90 && Alpha < 270) cosBeta = -cosBeta;
                        outputDirection = new Vector2((-mirrorHitNormal.x * cosBeta) - (sinBeta * -mirrorHitNormal.y), (-mirrorHitNormal.x * sinBeta) + (cosBeta * -mirrorHitNormal.y));
                        hitInfo = Physics2D.Raycast(mirrorHitPoint + outputDirection.normalized / 100, outputDirection, MaxRayDistance, LayerDetection);
                    }
                    if (insideGlassState)
                    {
                        Vector2 buforVector;
                        if (hitInfo.collider == null)
                            buforVector = mirrorHitPoint + outputDirection.normalized * MaxRayDistance;
                        else
                            buforVector = hitInfo.point;
                        hitInfo = Physics2D.Raycast(buforVector - outputDirection.normalized / 100, -outputDirection, MaxRayDistance, LayerDetection);
                    }
                    if (hitInfo.collider != null) mirrorLastHitPoint = mirrorHitPoint;


                }
                else break;
            }
            else
            {
                if (colision)
                {
                    LineOfSight.SetPosition(LineOfSight.positionCount - 1, mirrorHitPoint + outputDirection * MaxRayDistance );
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
            GlobalEvents.current.OnReciverEnter(this, new ReciverHitEventArgs() { laserindex = index, hitobject=_reciver });
        else if (!_reciverState && _reciverLastState)
            GlobalEvents.current.OnReciverExit(this, new ReciverHitEventArgs() { laserindex = index, hitobject = _reciver });

        _reciverLastState = _reciverState;
        _reciverState = false;

    }

    private void recLightRef(ref LineRenderer lineOfSight ,int mainReflectionsCount, int childsReflectionsCount, Vector2 direction, RaycastHit2D hitPoint, bool isItChildRay)
    {
        if (mainReflectionsCount < 0) return;
        lineOfSight.positionCount += 1;
        if(hitPoint.collider!= null) 
        {
            lineOfSight.SetPosition(lineOfSight.positionCount - 1, hitPoint.point);

        }

    }

}
