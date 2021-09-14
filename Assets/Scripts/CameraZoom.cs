using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Vector3 initPos;
    private Camera cam;
    private Vector3 givenPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ZoomInOut(Vector3 pos)
    {
        cam = gameObject.GetComponent<Camera>();
        givenPos = pos;
        givenPos.x = -0.75f;
        givenPos.y = -3.25f;
        givenPos.z = -10;
        InvokeRepeating("MoveTo", 0.0f, 0.01f);
        InvokeRepeating("EaseIn", 0.0f, 0.01f);
        //m_camera.orthographicSize = 1;
    }

    void MoveTo()
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / cam.orthographicSize * 1.0f);

        // Move camera
        transform.position += (givenPos - transform.position) * multiplier;

        // Zoom camera
        cam.orthographicSize -= 1.0f;

        // Limit zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1.0f, 5.0f);
    }

    void EaseIn()
    {
        if(cam.orthographicSize >= 1.5f)
            cam.orthographicSize -= 0.1f;
    }
}
