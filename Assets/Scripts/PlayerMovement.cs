using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal = 0;
    private float vertical = 0;
    public float horizontalSpeed;
    public float verticalSpeed;
    private Rigidbody2D rb;
    public Animator Animator;
    public bool canWalk = true;
    public bool canInteract;
    private GameObject item;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!GlobalReferences.Instance.isInInventory)
        {
            rb.velocity = new Vector2(horizontal * horizontalSpeed, vertical * verticalSpeed);
            if (rb.velocity != Vector2.zero)
            {

                Animator.SetBool("isWalking", true);
                Animator.SetFloat("MoveX", horizontal);
                Animator.SetFloat("MoveY", vertical);
            }
            else
            {
                Animator.SetBool("isWalking", false);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (canWalk)
        {
            horizontal = context.ReadValue<Vector2>().x;
            vertical = context.ReadValue<Vector2>().y;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("Interactable"))
        {
            canInteract = true;
            item = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("Interactable"))
        {
            canInteract = false;
        }
    }

    public void Interacting(InputAction.CallbackContext context)
    {
        if (canInteract && item != null)
        {
            if (item.CompareTag("InteractablePainting"))
            {
                item.GetComponent<InteractablePainting>()?.doWhat();
            }
            else if (item.CompareTag("InteractableKeyPad"))
            {
                item.GetComponent<KeyPad>()?.doWhat();
            }

            rb.velocity = Vector2.zero;
        }
    }

}

