using System;
using Unity.VisualScripting;
using UnityEngine;
using Laser;


public class LaserEmiter : MonoBehaviour
{
    public LineRenderer LineOfSight;
    public ParticleSystem particleSample;
    public int index;
    public double refractionFactor;
    public int reflections;
    public float MaxRayDistance;
    public LayerMask LayerDetection;
    [Range(1.0f, 2.0f)]
    public float lightTransmittingDencity;
    [Range(0.0f, 1.0f)]
    public float glow;
    [Range(0f,1f)]
    public float AbsorptionRate;

    private LaserHitParticles particles;
    private LaserBeam lines;
    private bool _reciverState = false;
    private bool _reciverLastState = false;
    private GameObject _reciver;

    // Start is called before the first frame update
    void Start()
    {
        particles = new LaserHitParticles(particleSample,LineOfSight.startColor);
        lines = new LaserBeam(LineOfSight);
        Physics2D.queriesStartInColliders = false;    
    }

    // Update is called once per frame
    void Update()
    {   
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, transform.right, MaxRayDistance, LayerDetection);
        bool colision = false;
        bool insideGlassState = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;
        Vector2 outputDirection = Vector2.zero;
        Vector2 mirrorLastHitPoint = (Vector2)transform.position;

        recLightRef(
            hitInfo, 
            mirrorLastHitPoint, 
            mirrorHitPoint, 
            mirrorHitNormal, 
            outputDirection, 
            colision, 
            insideGlassState, 
            reflections, 
            0.5f
            );

        if (_reciverState && !_reciverLastState)
            GlobalEvents.current.OnReciverEnter(this, new ReciverHitEventArgs() { laserindex = index, hitobject=_reciver });
        else if (!_reciverState && _reciverLastState)
            GlobalEvents.current.OnReciverExit(this, new ReciverHitEventArgs() { laserindex = index, hitobject = _reciver });
        _reciverLastState = _reciverState;
        _reciverState = false;
        lines.Update();
        particles.Delete(glow);

    }

    private void recLightRef(RaycastHit2D hitInfo, Vector2 mirrorLastHitPoint, Vector2 mirrorHitPoint, Vector2 mirrorHitNormal, Vector2 outputDirection, bool colision, bool insideGlass, int reflections, float power)
    {

        if(reflections>0 && power>0.01f)
        {
            if (hitInfo.collider != null)
            {
                mirrorHitPoint = hitInfo.point;
                mirrorHitNormal = hitInfo.normal;
                outputDirection = Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal);

                lines.SetLine(mirrorLastHitPoint, mirrorHitPoint, power);

                if (hitInfo.collider.CompareTag("Mirror"))
                {
                    particles.SetParticle(mirrorHitPoint, mirrorHitNormal);
                    hitInfo = Physics2D.Raycast(mirrorHitPoint, outputDirection, MaxRayDistance, LayerDetection);
                    if (hitInfo.collider != null) mirrorLastHitPoint = mirrorHitPoint;
                    colision = true;
                    recLightRef(hitInfo, mirrorLastHitPoint, mirrorHitPoint, mirrorHitNormal, outputDirection, colision, insideGlass, reflections - 1, power * AbsorptionRate);
                }
                else if (hitInfo.collider.CompareTag("Reciver"))
                {
                    _reciver = hitInfo.collider.GameObject();
                    GlobalEvents.current.OnReciverHit(this, new ReciverHitEventArgs() { laserindex = index, hitobject = _reciver });
                    _reciverState = true;
                }
                else if (hitInfo.collider.CompareTag("Glass"))
                {
                    colision = true;
                    Vector2 arrivalDirection = (mirrorHitPoint - mirrorLastHitPoint).normalized;
                    double Alpha = Vector2.SignedAngle(mirrorHitNormal, arrivalDirection) + 180;
                    double AlphaRad = Math.PI * Alpha / 180.0;
                    float sinBeta = (insideGlass) ? (float)Math.Sin(AlphaRad) / (float)Math.Pow(lightTransmittingDencity, -1) : (float)Math.Sin(AlphaRad) / lightTransmittingDencity;
                    var bufAng = (float)Math.Abs(Math.Abs(Alpha - 180) - 90) / 90f;

                    float aPower= 1.0f - bufAng, bPower=bufAng;

                    if (Math.Abs(sinBeta) > 1)
                    {
                        bPower = aPower;
                        aPower = bufAng;
                        outputDirection = Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal);
                        hitInfo = Physics2D.Raycast(mirrorHitPoint + outputDirection.normalized / 100, outputDirection, MaxRayDistance, LayerDetection);
                    }
                    else
                    {
                        float cosBeta = (float)Math.Sqrt(1 - Math.Pow(sinBeta, 2));

                        outputDirection = Vector2.Reflect((mirrorHitPoint - mirrorLastHitPoint).normalized, mirrorHitNormal);
                        hitInfo = Physics2D.Raycast(mirrorHitPoint + outputDirection.normalized / 100, outputDirection, MaxRayDistance, LayerDetection);
                        if (insideGlass) hitInfo = InsideGlassColison(hitInfo, mirrorHitPoint, outputDirection);
                        if (hitInfo.collider != null) mirrorLastHitPoint = mirrorHitPoint;
                        
                        Debug.Log(bufAng +", " + insideGlass);
                        recLightRef(hitInfo, mirrorLastHitPoint, mirrorHitPoint, mirrorHitNormal, outputDirection, colision, insideGlass, reflections - 1, power * aPower * AbsorptionRate);

                        insideGlass = !insideGlass;
                        if (Alpha > 90 && Alpha < 270) cosBeta = -cosBeta;
                        outputDirection = new Vector2((-mirrorHitNormal.x * cosBeta) - (sinBeta * -mirrorHitNormal.y), (-mirrorHitNormal.x * sinBeta) + (cosBeta * -mirrorHitNormal.y));
                        hitInfo = Physics2D.Raycast(mirrorHitPoint + outputDirection.normalized / 100, outputDirection, MaxRayDistance, LayerDetection);
                    }

                    if (insideGlass) hitInfo = InsideGlassColison(hitInfo, mirrorHitPoint, outputDirection);

                    if (hitInfo.collider != null) mirrorLastHitPoint = mirrorHitPoint;
                    recLightRef(hitInfo, mirrorLastHitPoint, mirrorHitPoint, mirrorHitNormal, outputDirection, colision, insideGlass, reflections - 1, power * bPower * AbsorptionRate);
                }
            }
            else
            {
                if (colision)
                {
                    lines.SetLine(mirrorHitPoint, mirrorHitPoint + outputDirection * MaxRayDistance, power);
                }
                else
                {
                    lines.SetLine(mirrorLastHitPoint, transform.position + transform.right * MaxRayDistance, power);
                }
            }
        }
    }

    private RaycastHit2D InsideGlassColison(RaycastHit2D hInfo, Vector2 hitPoint, Vector2 direction)
    {
        Vector2 buforVector;
        if (hInfo.collider == null)
            buforVector = hitPoint + direction.normalized * MaxRayDistance;
        else
            buforVector = hInfo.point;
        return Physics2D.Raycast(buforVector - direction.normalized / 100, -direction, MaxRayDistance, LayerDetection);
    }

}
