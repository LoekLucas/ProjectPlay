using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject inventoryScreen;
    public GameObject inventoryImage;
    public Sprite puzzle;
    public Sprite key;
    public Collider2D puzzleCollider;
    public Collider2D keyCollider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AcquireItem("Puzzle");
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            AcquireItem("Key");
        }
    }
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
    public void AcquireItem(string item)
    {
        if (item == "Puzzle")
        {
            inventoryImage.gameObject.GetComponent<Image>().overrideSprite = puzzle;
            //puzzleCollider.enabled = true;
        }
        if(item == "Key")
        {
            inventoryImage.gameObject.GetComponent<Image>().overrideSprite = key;
            //keyCollider.enabled = true;
        }
    }
}
