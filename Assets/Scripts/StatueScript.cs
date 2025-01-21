using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class StatueScript : MonoBehaviour
{

    public Button Statue1;
    public Button Statue2;
    public Button Statue3;
    public Button BackButton;

    public CameraZoom CameraZoom;
    private PlayerMovement playerMovement;

    public GameObject StatueMenu;
    public GameObject Door;
    public Component InteractionTrigger;

    public bool isOn;

    public int statue1Pose;
    public int statue2Pose;
    public int statue3Pose;



    void Start()
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
        StatueMenu.SetActive(true);
        CameraZoom.ZoomIn();
        if (isOn)
        {
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

    public void AddDigit(int statueNumber)
    {
        switch (statueNumber)
        {
            case 1:
                statue1Pose = (statue1Pose % 3) + 1;
                CheckCode();
                break;
            case 2:
                statue2Pose = (statue2Pose % 3) + 1;
                CheckCode();
                break;
            case 3:
                statue3Pose = (statue3Pose % 3) + 1;
                CheckCode();
                break;
        }
    }

    async void CheckCode()
    {
        if (statue1Pose == 3 && statue2Pose == 1 && statue3Pose == 2)
        {
            Debug.Log("Correct Code Entered!");
            await Task.Delay(2000);
            DisableInteraction();
            Destroy(InteractionTrigger);
            Destroy(Door);
            InteractionTrigger = null;
        }
        else
        {
            
        }
    }

    public void DisableInteraction()
    {
        StatueMenu.SetActive(false);
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