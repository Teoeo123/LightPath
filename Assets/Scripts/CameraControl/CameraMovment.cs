using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public float maxOffset=0.2f;
    public float respons = 0.1f;
    private float zstart;
    Vector3 cameraOffset;
    // Start is called before the first frame update
    private void Start()
    {
        zstart = transform.position.z;
        cameraOffset = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 buf = Camera.main.ScreenToWorldPoint(Input.mousePosition) * respons;
        buf.z = zstart;
        if (Mathf.Abs(buf.x)<maxOffset && Mathf.Abs(buf.y)<maxOffset)
            transform.position = buf +cameraOffset;
    }
}
