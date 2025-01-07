using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterractablePainting: MonoBehaviour
{
    public string whatToDo;

    public CameraZoom CameraZoom;

    public void doWhat()
    {
        switch (whatToDo)
        {
            case "Hi":
                break;
            case "Hi2":
                break;
            case "Hi3":
                break;
            case "Hi4":
                break;
            case "Hi5":
                break;
        }
        Debug.Log(whatToDo);
        CameraZoom.ZoomIn();
        Destroy(gameObject);
    }
}
