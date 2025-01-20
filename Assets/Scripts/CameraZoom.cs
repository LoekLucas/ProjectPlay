using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public float zoomFactor = 0.9f; // Factor to scale the zoom (For example,, 0.9 = zoom in by 10%)
    public float zoomSpeed = 30f;
    public float smoothTime = 0.3f;
    public bool useEasing = true;

    private float targetZoom;
    private float standardZoom;
    private float velocity = 0f;

    void Start()
    {
        standardZoom = cam.orthographicSize;
        targetZoom = standardZoom;
    }

    void Update()
    {
        if (useEasing)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref velocity, smoothTime);
        }
        else
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
        }
    }

    public void ZoomIn()
    {
        targetZoom *= zoomFactor;
    }

    public void ZoomOut()
    {
        targetZoom = standardZoom;
    }

    public void SetZoomSpeed(float speed)
    {
        zoomSpeed = speed;
    }

    public void SetZoomFactor(float factor)
    {
        zoomFactor = factor;
    }
}
