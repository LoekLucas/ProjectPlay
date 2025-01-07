using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public float maxZoom = 5;
    public float minZoom = 20;
    public float zoomStep = 1; // Amount to zoom in or out per call
    public float speed = 30;  // Smooth zooming speed
    private float targetZoom;

    void Start()
    {
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        // Smoothly interpolate the zoom level to the target zoom
        float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        cam.orthographicSize = newSize;
    }

    public void ZoomIn()
    {
        // Decrease the target zoom level (zoom in)
        targetZoom -= zoomStep;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    }

    public void ZoomOut()
    {
        // Increase the target zoom level (zoom out)
        targetZoom += zoomStep;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    }
}
