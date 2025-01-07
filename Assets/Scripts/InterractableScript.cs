using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterractableScript : MonoBehaviour
{
    public string whatToDo;

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
        Destroy(gameObject);
    }
}
