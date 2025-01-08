using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public float zoomFactor = 0.9f; // Factor to scale the zoom (for example, 0.9 = zoom in by 10%)
    public float zoomSpeed = 30f;
    private float targetZoom;
    private float standardZoom;

    void Start()
    {
        standardZoom = cam.orthographicSize;
        targetZoom = standardZoom;
    }

    void Update()
    {
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }

    public void ZoomIn()
    {
        targetZoom *= zoomFactor; // Zoom in by scaling down the target zoom
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
