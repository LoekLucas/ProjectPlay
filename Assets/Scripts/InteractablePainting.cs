using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractablePainting : MonoBehaviour
{
    public GameObject DarkOverlay;

    public GameObject Painting1;
    public GameObject Painting2;
    public GameObject Painting3;
    public GameObject Painting4;
    public GameObject Painting5;
    public GameObject Painting6;

    public string whatToDo;

    public CameraZoom CameraZoom;
    private PlayerMovement playerMovement;
    public bool isOn;

    

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        if (isOn)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isOn = false;
                DisableInteraction();
            }
        }
    }

    public void doWhat()
    {
        playerMovement.canInteract = false;
        isOn = true;
        DarkOverlay.SetActive(true);
        if (isOn)
        {
            switch (whatToDo)
            {
                case "P1":
                    Painting1.SetActive(true);
                    break;
                case "P2":
                    Painting2.SetActive(true);
                    break;
                case "P3":
                    Painting3.SetActive(true);
                    break;
                case "P4":
                    Painting4.SetActive(true);
                    break;
                case "P5":
                    Painting5.SetActive(true);
                    break;
            }
            Debug.Log(whatToDo);
            CameraZoom.ZoomIn();

            // Disable player movement and interaction when interacting
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
                playerMovement.canInteract = false;
            }
        }
        else
        {
            DisableInteraction();
            EnablePlayerInteraction();
        }
    }

    private void DisableInteraction()
    {
        Painting1.SetActive(false);
        Painting2.SetActive(false);
        Painting3.SetActive(false);
        Painting4.SetActive(false);
        Painting5.SetActive(false);
        Painting6.SetActive(false);
        DarkOverlay.SetActive(false);
        CameraZoom.ZoomOut();

        EnablePlayerInteraction();
    }

    private void EnablePlayerInteraction()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            playerMovement.canInteract = true;
        }
    }
}
