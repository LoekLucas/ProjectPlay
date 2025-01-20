using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{
    public GameObject inventoryScreen;
    public void OpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventoryScreen.activeInHierarchy)
            {
                inventoryScreen.SetActive(false);
                GlobalReferences.Instance.isInInventory = false;
            }
            else
            {
                inventoryScreen.SetActive(true);
                GlobalReferences.Instance.isInInventory = true;
            }

        }
        if (context.canceled)
        {
        }
    }
}
