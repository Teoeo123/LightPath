using UnityEngine;

public class fixRotation : MonoBehaviour
{
    
    public GameObject menuLocation;
    void Update()
    {
        transform.position = menuLocation.transform.position;
    }
}
