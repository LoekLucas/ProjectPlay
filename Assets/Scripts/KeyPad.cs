using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class KeyPad : MonoBehaviour
{
    public CameraZoom CameraZoom;
    private PlayerMovement playerMovement;

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;
    public Button Button8;
    public Button Button9;
    public Button Button0;
    public GameObject Keypad;
    public GameObject Door;
    public GameObject DarkOverlay;
    public Component InteractionTrigger;

    public TMP_Text InputText;

    public bool isOn;
    public string correctCode = "1234";

    private string enteredCode = "";

    // Start is called before the first frame update
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
                enteredCode = "";
                InputText.text = "";
                DisableInteraction();
            }
        }
    }

    public void doWhat()
    {
        playerMovement.canInteract = false;
        isOn = true;
        DarkOverlay.SetActive(true);
        Keypad.SetActive(true);
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

    public void AddDigit(int digit)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += digit;
            InputText.text = enteredCode;
        }

        if (enteredCode.Length == 4)
        {
            CheckCode();
        }
    }

    async void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Correct Code Entered!");
            InputText.color = Color.green;
            await Task.Delay(2000);
            DisableInteraction();
            Destroy(InteractionTrigger);
            Destroy(Door);
            InteractionTrigger = null;

        }
        else
        {
            Debug.Log("Incorrect Code. Try Again.");
            InputText.color = Color.red;
            await Task.Delay(2000);
            enteredCode = "";
            InputText.text = "";
            InputText.color = Color.white;
        }
    }

    private void DisableInteraction()
    {
        DarkOverlay.SetActive(false);
        Keypad.SetActive(false);
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


