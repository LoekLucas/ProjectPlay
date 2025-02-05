using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementLoek : MonoBehaviour
{
    private float horizontal = 0;
    private float vertical = 0;
    public float horizontalSpeed;
    public float verticalSpeed;
    private Rigidbody2D rb;
    public Animator Animator;
    public bool canWalk = true;
    private bool canInteract;
    private bool interacting;
    private GameObject item;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
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
        if (collision.gameObject.CompareTag("Box"))
        {
            canInteract = true;
            item = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            canInteract = false;
        }
    }
    public void Interacting(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            item.GetComponent<InteractablePainting>().doWhat();
            canInteract = false;
        }
    }
}