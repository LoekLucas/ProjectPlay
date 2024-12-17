using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed

    private Rigidbody2D rb; // Reference to Rigidbody2D
    private Vector2 moveInput; // Stores player input

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Normalize movement input to prevent diagonal speed boost
        moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }
}
